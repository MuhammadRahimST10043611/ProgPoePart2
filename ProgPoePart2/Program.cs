using System;

namespace ProgPoePart2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RecipeApp recipeApp = new RecipeApp();
            recipeApp.Run();

            // Keep the console window open
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
