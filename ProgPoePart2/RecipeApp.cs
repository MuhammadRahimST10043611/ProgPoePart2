using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgPoePart2
{
    // Class for managing recipes within the application
    class RecipeApp
    {
        // List to store recipes
        private List<Recipe> recipes;

        // Constructor to initialize the list of recipes
        public RecipeApp()
        {
            recipes = new List<Recipe>();
        }

        // Method to start the RecipeApp and present the main menu
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Culinary Compass App Menu");
                Console.WriteLine("1. Add Recipe.");
                Console.WriteLine("2. View Recipes.");
                Console.WriteLine("3. Scale Recipe.");
                Console.WriteLine("4. Remove Recipe.");
                Console.WriteLine("5. Edit Recipe.");
                Console.WriteLine("6. Exit.");
                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();

                // Switch statement to handle user choices
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
                        EditRecipeMenu();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        Pause();
                        break;
                }
            }
        }

        // Method to add a new recipe
        private void AddRecipe()
        {
            Recipe recipe = new Recipe();
            recipe.CalorieWarning += DisplayCalorieWarning; // Subscribe to the calorie warning event
            recipe.EnterRecipeDetails();
            recipes.Add(recipe);

            Console.WriteLine("\nRecipe added successfully!");
            Pause();
        }

        // Method to display all recipes
        private void ViewRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                Pause();
                return;
            }

            Console.WriteLine("\nRecipe List:");

            // Sort recipes by name in alphabetical order
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].Name}");
            }

            Recipe selectedRecipe = SelectRecipe(sortedRecipes);
            if (selectedRecipe != null)
            {
                DisplayRecipeDetails(selectedRecipe);
            }
        }

        // Method to scale a recipe
        private void ScaleRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                Pause();
                return;
            }

            Console.WriteLine("\nAvailable Recipes:");

            // Sort recipes by name in alphabetical order
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].Name}");
            }

            Recipe recipe = SelectRecipe(sortedRecipes);
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

        // Method to remove a recipe
        private void RemoveRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                Pause();
                return;
            }

            Console.WriteLine("\nAvailable Recipes:");

            // Sort recipes by name in alphabetical order
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].Name}");
            }

            Recipe recipe = SelectRecipe(sortedRecipes);
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

        // Method to edit a recipe
        private void EditRecipeMenu()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                Pause();
                return;
            }

            Console.WriteLine("\nAvailable Recipes:");

            // Sort recipes by name in alphabetical order
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipes[i].Name}");
            }

            Recipe recipe = SelectRecipe(sortedRecipes);
            if (recipe != null)
            {
                recipe.EditRecipe();
            }
            else
            {
                Console.WriteLine("\nRecipe not found.");
            }
            Pause();
        }

        // Method to select a recipe from a list
        private Recipe SelectRecipe(List<Recipe> sortedRecipes)
        {
            Console.Write("\nEnter the number of the recipe you want to select: ");
            if (int.TryParse(Console.ReadLine(), out int recipeIndex) && recipeIndex > 0 && recipeIndex <= sortedRecipes.Count)
            {
                return sortedRecipes[recipeIndex - 1];
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                return null;
            }
        }

        // Method to display recipe details
        private void DisplayRecipeDetails(Recipe recipe)
        {
            Console.WriteLine("\nRecipe Details:");
            recipe.DisplayRecipe();
            double totalCalories = recipe.CalculateTotalCalories();
            Console.WriteLine($"Total Calories: {totalCalories}");

            recipe.NotifyIfCaloriesExceedLimit(); // Check if calories exceed the limit
            Pause();
        }

        // Method to display calorie warning
        private void DisplayCalorieWarning(string message)
        {
            Console.WriteLine(message);
        }

        // Method to pause and prompt for key press
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
