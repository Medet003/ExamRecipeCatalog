using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public List<string> Instructions { get; set; }
    public string? PhotoUrl { get; set; }
    public double Rating { get; set; } // Добавлено свойство Rating
}

public class RecipeManager
{
    private List<Recipe> recipes;

    public RecipeManager()
    {
        recipes = new List<Recipe>();
        LoadRecipesFromFile(); // Загружаем рецепты из файла при создании экземпляра класса
    }

    public void AddRecipe()
    {
        Recipe newRecipe = new Recipe();

        Console.Write("Введите название рецепта: ");
        newRecipe.Name = Console.ReadLine();

        newRecipe.Ingredients = GetIngredients();
        newRecipe.Instructions = GetInstructions();

        Console.Write("Введите URL фотографии блюда (если есть): ");
        newRecipe.PhotoUrl = Console.ReadLine();

        // Генерируем уникальный ID для рецепта
        newRecipe.Id = recipes.Count + 1;
        newRecipe.Rating = 0; // Исходно устанавливаем рейтинг в 0

        recipes.Add(newRecipe);
        SaveRecipesToFile(); // Сохраняем рецепты в файл после добавления нового рецепта
        Console.WriteLine("Рецепт успешно добавлен!");
    }

    public void ViewAllRecipes()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("Нет доступных рецептов.");
            return;
        }

        foreach (var recipe in recipes)
        {
            Console.WriteLine($"ID: {recipe.Id}");
            Console.WriteLine($"Название: {recipe.Name}");
            Console.WriteLine($"Ингредиенты: {string.Join(", ", recipe.Ingredients)}");
            Console.WriteLine($"Инструкции: {string.Join(", ", recipe.Instructions)}");
            Console.WriteLine($"Фото: {recipe.PhotoUrl}");
            Console.WriteLine($"Рейтинг: {recipe.Rating}");
            Console.WriteLine();
        }
    }

    public void SearchRecipeByName()
    {
        Console.Write("Введите название блюда для поиска: ");
        string searchName = Console.ReadLine();

        var matchingRecipes = recipes.FindAll(recipe => recipe.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));

        if (matchingRecipes.Count > 0)
        {
            Console.WriteLine("Найденные рецепты:");
            ViewAllRecipes(matchingRecipes);
        }
        else
        {
            Console.WriteLine("Рецепты с таким названием не найдены.");
        }
    }

    public void RemoveRecipe()
    {
        Console.Write("Введите ID рецепта для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int recipeId))
        {
            Recipe recipeToRemove = recipes.Find(recipe => recipe.Id == recipeId);
            if (recipeToRemove != null)
            {
                recipes.Remove(recipeToRemove);
                SaveRecipesToFile(); // Сохраняем рецепты в файл после удаления рецепта
                Console.WriteLine("Рецепт успешно удален!");
            }
            else
            {
                Console.WriteLine("Рецепт с указанным ID не найден.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод ID рецепта.");
        }
    }

    public void SortRecipes(string sortBy)
    {
        switch (sortBy.ToLower())
        {
            case "date":
                recipes.Sort((r1, r2) => r2.Id.CompareTo(r1.Id));
                break;
            case "name":
                recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name, StringComparison.OrdinalIgnoreCase));
                break;
            default:
                Console.WriteLine("Некорректный критерий сортировки.");
                break;
        }
    }

    public void RateRecipe()
    {
        Console.Write("Введите ID рецепта для оценки: ");
        if (int.TryParse(Console.ReadLine(), out int recipeId))
        {
            Recipe recipeToRate = recipes.Find(recipe => recipe.Id == recipeId);
            if (recipeToRate != null)
            {
                Console.Write("Введите оценку (от 1 до 5): ");
                if (double.TryParse(Console.ReadLine(), out double rating) && rating >= 1 && rating <= 5)
                {
                    recipeToRate.Rating = rating;
                    SaveRecipesToFile(); // Сохраняем рецепты в файл после изменения рейтинга
                    Console.WriteLine("Рецепт успешно оценен!");
                }
                else
                {
                    Console.WriteLine("Некорректная оценка. Оценка должна быть числом от 1 до 5.");
                }
            }
            else
            {
                Console.WriteLine("Рецепт с указанным ID не найден.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод ID рецепта.");
        }
    }

    private List<string> GetIngredients()
    {
        List<string> ingredients = new List<string>();
        Console.WriteLine("Введите ингредиенты (введите 'готово', чтобы завершить):");

        while (true)
        {
            string ingredient = Console.ReadLine();
            if (ingredient.ToLower() == "готово")
            {
                break;
            }
            ingredients.Add(ingredient);
        }

        return ingredients;
    }

    private List<string> GetInstructions()
    {
        List<string> instructions = new List<string>();
        Console.WriteLine("Введите инструкции (введите 'готово', чтобы завершить):");

        while (true)
        {
            string instruction = Console.ReadLine();
            if (instruction.ToLower() == "готово")
            {
                break;
            }
            instructions.Add(instruction);
        }

        return instructions;
    }

    private void SaveRecipesToFile()
    {
        string json = JsonSerializer.Serialize(recipes);
        File.WriteAllText("recipes.json", json);
    }

    private void LoadRecipesFromFile()
    {
        if (File.Exists("recipes.json"))
        {
            string json = File.ReadAllText("recipes.json");
            recipes = JsonSerializer.Deserialize<List<Recipe>>(json);
        }
    }

    public void DisplayRecipes(List<Recipe> recipesToDisplay)
    {
        if (recipesToDisplay.Count > 0)
        {
            foreach (var recipe in recipesToDisplay)
            {
                Console.WriteLine($"ID: {recipe.Id}");
                Console.WriteLine($"Название: {recipe.Name}");
                Console.WriteLine($"Ингредиенты: {string.Join(", ", recipe.Ingredients)}");
                Console.WriteLine($"Инструкции: {string.Join(", ", recipe.Instructions)}");
                Console.WriteLine($"Фото: {recipe.PhotoUrl}");
                Console.WriteLine($"Рейтинг: {recipe.Rating}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Нет доступных рецептов.");
        }
    }
}
