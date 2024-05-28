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
            bool addMoreRecipes;
            do
            {
                Recipe recipe = new Recipe();
                recipe.EnterRecipeDetails();
                recipes.Add(recipe);

                Console.Write("\nWould you like to add another recipe? (yes/no) ");
                string response = Console.ReadLine().ToLower();
                addMoreRecipes = response == "yes";
            } while (addMoreRecipes);

            DisplayRecipeList();
            DisplayRecipeDetails();
        }

        public void DisplayRecipeList()
        {
            Console.WriteLine("\nRecipe List:");
            foreach (Recipe recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }
        }

        public void DisplayRecipeDetails()
        {
            Recipe recipe;
            do
            {
                Console.Write("\nEnter the name of the recipe you want to display: ");
                string recipeName = Console.ReadLine();
                recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe == null)
                {
                    Console.WriteLine("Recipe not found. Would you like to try again? (yes/no)");
                    string tryAgainResponse = Console.ReadLine().ToLower();
                    if (tryAgainResponse != "yes")
                        return; // Exit the method if the user doesn't want to try again
                }
            } while (recipe == null);

            Console.WriteLine("\nBefore scaling the recipe:");
            recipe.DisplayRecipe();
            double totalCalories = recipe.CalculateTotalCalories();
            Console.WriteLine($"Total Calories: {totalCalories}");

            if (totalCalories > 300)
            {
                Console.WriteLine("Warning: Total calories exceed 300!");
            }

            bool scaleMore;
            do
            {
                bool scaled = recipe.ScaleRecipe();
                if (scaled)
                {
                    Console.WriteLine("\nAfter scaling the recipe:");
                    recipe.DisplayRecipe();
                    totalCalories = recipe.CalculateTotalCalories();
                    Console.WriteLine($"Total Calories: {totalCalories}");

                    if (totalCalories > 300)
                    {
                        Console.WriteLine("Warning: Total calories exceed 300!");
                    }
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

            // Clear the recipe if you want to remove it after displaying
            recipe.ClearRecipe();
        }
    }
    }
