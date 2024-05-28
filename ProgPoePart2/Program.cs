using System;

namespace ProgPoePart2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcomeScreen(); // Display welcome screen
            Console.ReadLine(); // Wait for user to press Enter

            Console.ForegroundColor = ConsoleColor.Yellow; // Set text color to green
            RecipeApp recipeApp = new RecipeApp();
            recipeApp.Run();

            DisplayExitScreen(); // Display exit screen

            // Keep the console window open
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan; // Set text color to green
            Console.WriteLine(@" 
                W   W  EEEE  L     CCCC   OOO  M   M  EEEE
                W   W  E     L     C     O   O MM MM  E    
                W W W  EEEE  L     C     O   O M M M  EEEE
                WW WW  E     L     C     O   O M   M  E    
                W   W  EEEE  LLLLL  CCCC  OOO  M   M  EEEE                                   
            ");
            Console.WriteLine("Welcome to the Culinary Compass App!\n");
            Console.WriteLine("Press Enter to continue...");
        }

        static void DisplayExitScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan; // Set text color to green
            Console.WriteLine(@" 
                BBBB   YY   YY  EEEE
                B   B   YY YY   E   
                BBBB     Y     EEEE
                B   B    Y     E   
                BBBB     Y     EEEE

            ");
            Console.WriteLine("Thank you for using the Culinary Compass App!");
        }
    }
}
