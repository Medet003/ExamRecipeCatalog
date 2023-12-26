using System.Collections.Generic;

public class RecipeContext
{
    public List<Recipe> Recipes { get; set; }

    public RecipeContext()
    {
        Recipes = new List<Recipe>();
    }
}


