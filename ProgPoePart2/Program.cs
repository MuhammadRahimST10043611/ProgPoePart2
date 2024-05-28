using System;

namespace ProgPoePart2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcomeScreen(); // Display welcome screen
            Console.ReadLine(); // Wait for user to press Enter

            Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
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
            Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
            Console.WriteLine(@" 
  ____                   _               _____      _   _     
 / ___| ___   ___   __ _| | ___ _ __    |_   _|   _| |_| |__  
| |  _ / _ \ / _ \ / _` | |/ _ \ '__|     | || | | | __| '_ \ 
| |_| | (_) | (_) | (_| | |  __/ |       | || |_| | |_| | | |
 \____|\___/ \___/ \__, |_|\___|_|       |_| \__,_|\__|_| |_|
                    |___/                                     
            ");
            Console.WriteLine("Welcome to the Culinary Compass App!\n");
            Console.WriteLine("Press Enter to continue...");
        }

        static void DisplayExitScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
            Console.WriteLine(@" 
   ____                              _         
  / ___| __ _ _ __ ___   ___    __ _| | _____  
 | |  _ / _` | '_ ` _ \ / _ \  / _` | |/ / _ \ 
 | |_| | (_| | | | | | |  __/ | (_| |   <  __/ 
  \____|\__,_|_| |_| |_|\___|  \__,_|_|\_\___|
                                               
            ");
            Console.WriteLine("Thank you for using the Culinary Compass App!");
        }
    }
}
