// RecipeService.cs (в папке Services)

using System;
using System.Collections.Generic;

public class RecipeService
{
    private List<Recipe> recipes;

    public RecipeService()
    {
        recipes = new List<Recipe>();
    }

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
    }

    public List<Recipe> GetAllRecipes()
    {
        return recipes;
    }
}


    }

    // Получение рецепта по ID
    public Recipe? GetRecipeById(int id)
    {
        return dbContext.Recipes.FirstOrDefault(r => r.Id == id);
    }

    // Удаление рецепта по ID
    public void RemoveRecipe(int id)
    {
        var recipeToRemove = dbContext.Recipes.FirstOrDefault(r => r.Id == id);
        if (recipeToRemove != null)
        {
            dbContext.Recipes.Remove(recipeToRemove);
            dbContext.SaveChanges();
        }
    }

    // Сортировка рецептов по указанному критерию
    private List<Recipe> SortRecipes(string sortBy)
    {
        switch (sortBy)
        {
            case "date":
                return dbContext.Recipes.OrderByDescending(r => r.CreatedAt).ToList();
            case "name":
                return dbContext.Recipes.OrderBy(r => r.Name).ToList();
            default:
                return dbContext.Recipes.ToList();
        }
    }

    // Сохранение рецептов в файл
    public void SaveToFile(string filePath)
    {
        // Реализуйте сохранение рецептов в файл по указанному пути
    }

    // Загрузка рецептов из файла
    public void LoadFromFile(string filePath)
    {
        // Реализуйте загрузку рецептов из файла по указанному пути
    }

    // Дополнительная задача: Оценка рецепта
    public void RateRecipe(int recipeId, double rating)
    {
        var recipeToRate = dbContext.Recipes.FirstOrDefault(r => r.Id == recipeId);
        if (recipeToRate != null)
        {
            recipeToRate.Rating = rating;
            dbContext.SaveChanges();
        }
    }

    // Дополнительная задача: Отображение списка рецептов
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



