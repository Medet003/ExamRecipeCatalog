using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        // Создаем экземпляр RecipeContext
        RecipeContext recipeContext = new RecipeContext();

        // Запускаем консольное приложение
        ConsoleView consoleView = new ConsoleView(recipeContext);
        consoleView.Start();
    }

    public class RecipeContext
    {
        public List<Recipe> Recipes { get; set; }

        public RecipeContext()
        {
            Recipes = new List<Recipe>();
        }
    }

    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public string? PhotoUrl { get; set; }
        public double Rating { get; set; }
    }

    public class RecipeService
    {
        private RecipeContext dbContext;

        public RecipeService(RecipeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddRecipe(Recipe recipe)
        {
            dbContext.Recipes.Add(recipe);
        }

        public List<Recipe> GetAllRecipes()
        {
            return dbContext.Recipes;
        }

        public Recipe GetRecipeById(int id)
        {
            return dbContext.Recipes.FirstOrDefault(r => r.Id == id);
        }

        public void RemoveRecipe(int id)
        {
            var recipeToRemove = dbContext.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipeToRemove != null)
            {
                dbContext.Recipes.Remove(recipeToRemove);
            }
        }

        public List<Recipe> SortRecipes(string sortBy)
        {
            switch (sortBy)
            {
                case "date":
                    return dbContext.Recipes.OrderByDescending(r => r.Id).ToList();
                case "name":
                    return dbContext.Recipes.OrderBy(r => r.Name).ToList();
                default:
                    return dbContext.Recipes.ToList();
            }
        }

        public void SaveToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(dbContext.Recipes);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                dbContext.Recipes = JsonSerializer.Deserialize<List<Recipe>>(json);
            }
        }

        public void RateRecipe(int recipeId, double rating)
        {
            var recipeToRate = dbContext.Recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipeToRate != null)
            {
                recipeToRate.Rating = rating;
            }
        }

        public void DisplayRecipes(List<Recipe> recipes)
        {
            if (recipes.Any())
            {
                foreach (var recipe in recipes)
                {
                    Console.WriteLine($"ID: {recipe.Id}");
                    Console.WriteLine($"Название: {recipe.Name}");
                    Console.WriteLine($"Добавлен: {recipe.CreatedAt}");
                    // Дополнительные свойства для вывода при необходимости
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Нет доступных рецептов.");
            }
        }
    }

    public class ConsoleView
    {
        private RecipeService recipeService;

        public ConsoleView(RecipeContext recipeContext)
        {
            recipeService = new RecipeService(recipeContext);
        }

        public void Start()
        {
            AddDefaultRecipes(); // Вызываем метод AddDefaultRecipes при старте приложения

            while (true)
            {
                Console.WriteLine("1. Добавить новый рецепт");
                Console.WriteLine("2. Просмотреть все рецепты");
                Console.WriteLine("3. Получить рецепт по ID");
                Console.WriteLine("4. Удалить рецепт по ID");
                Console.WriteLine("5. Сортировать рецепты");
                Console.WriteLine("6. Сохранить рецепты в файл");
                Console.WriteLine("7. Загрузить рецепты из файла");
                Console.WriteLine("8. Выйти");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewRecipe();
                        break;
                    case "2":
                        DisplayAllRecipes();
                        break;
                    case "3":
                        GetRecipeById();
                        break;
                    case "4":
                        DeleteRecipe();
                        break;
                    case "5":
                        SortRecipes();
                        break;
                    case "6":
                        SaveRecipesToFile();
                        break;
                    case "7":
                        LoadRecipesFromFile();
                        break;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, введите корректное значение.");
                        break;
                }
            }
        }

        private void AddDefaultRecipes()
        {
            // Предопределенные рецепты, добавляемые при старте приложения
            Recipe recipe1 = new Recipe
            {
                Name = "Паста карбонара",
                Ingredients = new List<string> { "Спагетти, яйца, гуанчиале, сыр пармезан, соль, перец" },
                Instructions = new List<string> { "1. Обжарьте нарезанный гуанчиале. 2. Приготовьте спагетти. 3. Смешайте яйца, сыр, гуанчиале. 4. Добавьте смесь к спагетти и перемешайте. 5. Подавайте с перцем." }
            };
            Recipe recipe2 = new Recipe
            {
                Name = "Греческий салат",
                Ingredients = new List<string> { "Помидоры, огурцы, красный лук, маслины, сыр фета, оливковое масло, уксус, соль, перец, орегано" },
                Instructions = new List<string> { "1. Нарежьте помидоры, огурцы, лук, добавьте маслины. 2. Нарежьте сыр фета. 3. Смешайте оливковое масло, уксус, соль, перец, орегано. 4. Полейте салат соусом." }
            };

            // Добавляем предопределенные рецепты в сервис
            recipeService.AddRecipe(recipe1);
            recipeService.AddRecipe(recipe2);

            Console.WriteLine("Предопределенные рецепты успешно добавлены!");
        }

        private void AddNewRecipe()
        {
            // Код для добавления нового рецепта
            Console.WriteLine("Введите данные для нового рецепта:");
            Recipe newRecipe = new Recipe();

            Console.Write("Введите название рецепта: ");
            newRecipe.Name = Console.ReadLine();

            newRecipe.Ingredients = GetIngredients();
            newRecipe.Instructions = GetInstructions();

            Console.Write("Введите URL фотографии блюда (если есть): ");
            newRecipe.PhotoUrl = Console.ReadLine();

            // Генерируем уникальный ID для рецепта
            newRecipe.Id = recipeService.GetAllRecipes().Count + 1;
            newRecipe.Rating = 0; // Исходно устанавливаем рейтинг в 0

            recipeService.AddRecipe(newRecipe);
            Console.WriteLine("Рецепт успешно добавлен!");
        }

        private void DisplayAllRecipes()
        {
            // Код для вывода всех рецептов
            List<Recipe> allRecipes = recipeService.GetAllRecipes();
            recipeService.DisplayRecipes(allRecipes);
        }

        private void GetRecipeById()
        {
            // Код для получения рецепта по ID
            Console.Write("Введите ID рецепта: ");
            if (int.TryParse(Console.ReadLine(), out int recipeId))
            {
                Recipe recipe = recipeService.GetRecipeById(recipeId);
                if (recipe != null)
                {
                    Console.WriteLine($"Найден рецепт:");
                    recipeService.DisplayRecipes(new List<Recipe> { recipe });
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

        private void DeleteRecipe()
        {
            // Код для удаления рецепта
            Console.Write("Введите ID рецепта для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int recipeId))
            {
                recipeService.RemoveRecipe(recipeId);
                Console.WriteLine("Рецепт успешно удален!");
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID рецепта.");
            }
        }

        private void SortRecipes()
        {
            // Код для сортировки рецептов
            Console.Write("Выберите критерий сортировки (date или name): ");
            string sortBy = Console.ReadLine().ToLower();
            List<Recipe> sortedRecipes = recipeService.SortRecipes(sortBy);
            recipeService.DisplayRecipes(sortedRecipes);
        }

        private List<string> GetIngredients()
        {
            // Код для ввода ингредиентов
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
            // Код для ввода инструкций
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
            // Код для сохранения рецептов в файл
            Console.Write("C:\Users\Ad ");
            string filePath = Console.ReadLine();
            recipeService.SaveToFile(filePath);
            Console.WriteLine("Рецепты успешно сохранены в файл.");
        }

        private void LoadRecipesFromFile()
        {
            // Код для загрузки рецептов из файла
            Console.Write("Введите путь к файлу для загрузки рецептов: ");
            string filePath = Console.ReadLine();
            recipeService.LoadFromFile(filePath);
            Console.WriteLine("Рецепты успешно загружены из файла.");
        }
    }
}
