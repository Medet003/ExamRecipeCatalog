

using System;

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
        public static void AddRecipes(RecipeService recipeService)
        {
            // Рецепт 1
            Recipe recipe1 = new Recipe
            {
                Name = "Картошка по-деревенски",
                Ingredients = new List<string> { "Картошка, масло, соль, перец, паприка" },
                Instructions = new List<string> { "1. Нарежьте картошку дольками. 2. Обсыпьте специями. 3. Запекайте в духовке." }
            };
            recipeService.AddRecipe(recipe1);

            // Рецепт 2
            Recipe recipe2 = new Recipe
            {
                Name = "Салат Цезарь",
                Ingredients = new List<string> { "Куриная грудка, салат романо, хлебные кубики, пармезан, соус Цезарь", },
                Instructions = new List<string> { "1. Обжарьте куриную грудку. 2. Подготовьте салатные листья, добавьте хлебные кубики и пармезан. 3. Добавьте обжаренную курицу и соус Цезарь." }
            };
            recipeService.AddRecipe(recipe2);

            // Рецепт 3
            Recipe recipe3 = new Recipe
            {
                Name = "Лазанья",
                Ingredients = new List<string> { "Лазанья, мясной фарш, лук, чеснок, томатный соус, моцарелла, пармезан" },
                Instructions = new List<string> { "1. Обжарьте лук и чеснок, добавьте мясной фарш. 2. Добавьте томатный соус. 3. Соберите слои лазаньи с начинкой, моцареллой и пармезаном." }
            };
            recipeService.AddRecipe(recipe3);

            // Рецепт 4
            Recipe recipe4 = new Recipe
            {
                Name = "Омлет с овощами",
                Ingredients = new List<string> { "Яйца, помидоры, перец, лук, соль, перец, зелень" },
                Instructions = new List<string> { "1. Обжарьте лук, добавьте нарезанные помидоры и перец. 2. Взбейте яйца и добавьте к овощам. 3. Подсолите, поперчите, посыпьте зеленью." }
            };
            recipeService.AddRecipe(recipe4);

            // Рецепт 5
            Recipe recipe5 = new Recipe
            {
                Name = "Суп-гуляш",
                Ingredients = new List<string> { "Говядина, лук, картошка, морковь, томатная паста, специи" },
                Instructions = new List<string> { "1. Обжарьте лук, добавьте мясо и обжаривайте до золотистости. 2. Добавьте картошку, морковь, томатную пасту. 3. Добавьте специи и тушите до готовности." }
            };
            recipeService.AddRecipe(recipe5);

            // Рецепт 6
            Recipe recipe6 = new Recipe
            {
                Name = "Спагетти Болоньезе",
                Ingredients = new List<string> { "Фарш, лук, чеснок, томатный соус, спагетти, оливковое масло" },
                Instructions = new List<string> { "1. Обжарьте лук и чеснок, добавьте фарш. 2. Добавьте томатный соус и тушите. 3. Приготовьте спагетти и подавайте с соусом." }
            };
            recipeService.AddRecipe(recipe6);

            // Рецепт 7
            Recipe recipe7 = new Recipe
            {
                Name = "Курица терияки",
                Ingredients = new List<string> { "Куриные крылышки, соус терияки, мед, соль, перец" },
                Instructions = new List<string> { "1. Обжарьте куриные крылышки до золотистости. 2. Приготовьте соус терияки с медом. 3. Обильно полейте крылышки соусом и запекайте." }
            };
            recipeService.AddRecipe(recipe7);

            // Рецепт 8
            Recipe recipe8 = new Recipe
            {
                Name = "Фруктовый салат",
                Ingredients = new List<string> { "Яблоки, бананы, виноград, апельсины, мед, мятная заправка" },
                Instructions = new List<string> { "1. Нарежьте фрукты. 2. Приготовьте мятную заправку из меда и мелко нарубленной мяты. 3. Перемешайте фрукты с заправкой." }
            };
            recipeService.AddRecipe(recipe8);

            // Рецепт 9
            Recipe recipe9 = new Recipe
            {
                Name = "Печенье шоколадное",
                Ingredients = new List<string> { "Мука, какао, сахар, масло, яйцо, шоколадные капли" },
                Instructions = new List<string> { "1. Смешайте муку, какао и сахар. 2. Добавьте масло и яйцо, перемешайте. 3. Добавьте шоколадные капли и испеките печенье." }
            };
            recipeService.AddRecipe(recipe9);

            // Рецепт 10
            Recipe recipe10 = new Recipe
            {
                Name = "Чай масала",
                Ingredients = new List<string> { "Черный чай, молоко, корица, имбирь, кардамон, мед" },
                Instructions = new List<string> { "1. Варите черный чай в молоке. 2. Добавьте корицу, имбирь и кардамон. 3. Подсластите медом." }
            };
            recipeService.AddRecipe(recipe10);

        }
    }



}



    