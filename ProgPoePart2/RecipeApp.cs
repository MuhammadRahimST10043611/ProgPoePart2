using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgPoePart2
{
    class RecipeApp
    {
        private List<Recipe> recipes;

        public RecipeApp()
        {
            recipes = new List<Recipe>();
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Recipe App Menu");
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. View Recipes");
                Console.WriteLine("3. Scale Recipe");
                Console.WriteLine("4. Remove Recipe");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        ViewRecipes();
                        break;
                    case "3":
                        ScaleRecipe();
                        break;
                    case "4":
                        RemoveRecipe();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        Pause();
                        break;
                }
            }
        }

        private void AddRecipe()
        {
            Recipe recipe = new Recipe();
            recipe.CalorieWarning += DisplayCalorieWarning; // Subscribe to the calorie warning event
            recipe.EnterRecipeDetails();
            recipes.Add(recipe);

            Console.WriteLine("\nRecipe added successfully!");
            Pause();
        }

        private void ViewRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                Pause();
                return;
            }

            Console.WriteLine("\nRecipe List:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Recipe selectedRecipe = SelectRecipe();
            if (selectedRecipe != null)
            {
                DisplayRecipeDetails(selectedRecipe);
            }
        }

        private void ScaleRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                Pause();
                return;
            }

            Recipe recipe = SelectRecipe();
            if (recipe != null)
            {
                bool scaleMore;
                do
                {
                    bool scaled = recipe.ScaleRecipe();
                    if (scaled)
                    {
                        Console.WriteLine("\nAfter scaling the recipe:");
                        recipe.DisplayRecipe();
                        double totalCalories = recipe.CalculateTotalCalories();
                        Console.WriteLine($"Total Calories: {totalCalories}");

                        recipe.NotifyIfCaloriesExceedLimit(); // Check if calories exceed the limit after scaling
                    }
                    else
                    {
                        break; // Exit the loop if the user does not want to scale
                    }

                    Console.Write("\nWould you like to scale the recipe again? (yes/no) ");
                    string response = Console.ReadLine().ToLower();
                    scaleMore = response == "yes";
                } while (scaleMore);

                // Reset to original quantities and calories after all scaling is done
                recipe.ResetQuantities();
                recipe.ResetCalories();
                Console.WriteLine("\nResetting to original quantities and calories.");
                recipe.DisplayRecipe();
            }
            else
            {
                Console.WriteLine("\nRecipe not found.");
            }
            Pause();
        }

        private void RemoveRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                Pause();
                return;
            }

            Recipe recipe = SelectRecipe();
            if (recipe != null)
            {
                recipes.Remove(recipe);
                Console.WriteLine("\nRecipe removed successfully!");
            }
            else
            {
                Console.WriteLine("\nRecipe not found.");
            }
            Pause();
        }

        private Recipe SelectRecipe()
        {
            Console.Write("\nEnter the number of the recipe you want to select: ");
            if (int.TryParse(Console.ReadLine(), out int recipeIndex) && recipeIndex > 0 && recipeIndex <= recipes.Count)
            {
                return recipes[recipeIndex - 1];
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            Console.WriteLine("\nRecipe Details:");
            recipe.DisplayRecipe();
            double totalCalories = recipe.CalculateTotalCalories();
            Console.WriteLine($"Total Calories: {totalCalories}");

            recipe.NotifyIfCaloriesExceedLimit(); // Check if calories exceed the limit
            Pause();
        }

        private void DisplayCalorieWarning(string message)
        {
            Console.WriteLine(message);
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
