using ProgPoePart2;
using System;
using System.Collections.Generic;
using System.Linq;

class Recipe
{
    // Properties to store recipe details
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<string> Steps { get; set; }
    private List<double> originalCalories;
    private List<string> originalQuantities;

    // Delegate to notify when calories exceed 300
    public delegate void CalorieWarningHandler(string message);
    public event CalorieWarningHandler CalorieWarning;

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
        Steps = new List<string>();
        originalCalories = new List<double>();
        originalQuantities = new List<string>();
    }

    // Method to enter recipe details
    public void EnterRecipeDetails()
    {
        Console.Write("\nEnter recipe name: ");
        Name = Console.ReadLine();

        int numIngredients = ReadInt("Enter the number of ingredients: ");
        for (int i = 0; i < numIngredients; i++)
        {
            Console.WriteLine($"\nIngredient {i + 1}:");
            Ingredient ingredient = new Ingredient
            {
                Name = ReadString("Enter ingredient name: "),
                Quantity = ReadDouble("Enter quantity: "),
                Unit = ReadString("Enter unit of measurement: "),
                Calories = ReadDouble("Enter number of calories: "),
                FoodGroup = ReadString("Enter food group: ")
            };
            Ingredients.Add(ingredient);
            originalQuantities.Add($"{ingredient.Quantity} {ingredient.Unit}"); // Store the original quantity
            originalCalories.Add(ingredient.Calories); // Store the original calorie value
        }

        int numSteps = ReadInt("\nEnter the number of steps: ");
        for (int i = 0; i < numSteps; i++)
        {
            Console.Write($"Enter step {i + 1}: ");
            Steps.Add(Console.ReadLine());
        }
    }

    // Method to display recipe details
    public void DisplayRecipe()
    {
        Console.WriteLine($"\nRecipe: {Name}");
        Console.WriteLine("\nIngredients:");
        foreach (Ingredient ingredient in Ingredients)
        {
            Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
        }
        Console.WriteLine("\nSteps:");
        foreach (string step in Steps)
        {
            Console.WriteLine(step);
        }
    }

    // Method to calculate total calories of the recipe
    public double CalculateTotalCalories()
    {
        double totalCalories = Ingredients.Sum(ingredient => ingredient.Calories);
        return totalCalories;
    }

    // Method to scale the recipe
    public bool ScaleRecipe()
    {
        Console.Write("\nWould you like to scale the recipe? (yes/no) ");
        string response = Console.ReadLine().ToLower();
        if (response == "yes")
        {
            double scaleFactor = ReadDouble("Enter the scaling factor (0.5, 2, or 3): ", new[] { 0.5, 2.0, 3.0 });

            ResetQuantities(); // Reset to original quantities before scaling again
            ResetCalories(); // Reset to original calorie values before scaling again

            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantity *= scaleFactor;
                Ingredients[i].Calories = originalCalories[i] * scaleFactor; // Scale calories based on original values
            }

            return true; // Indicate that scaling was done
        }
        else if (response == "no")
        {
            return false; // Indicate that scaling was not done
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter yes or no.");
            return ScaleRecipe();
        }
    }

    // Method to reset ingredient quantities to their original values
    public void ResetQuantities()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            string[] parts = originalQuantities[i].Split(' ');
            if (double.TryParse(parts[0], out double quantity))
            {
                Ingredients[i].Quantity = quantity;
            }
            else
            {
                Console.WriteLine($"Failed to parse quantity for ingredient: {originalQuantities[i]}");
            }
        }

    }

    // Method to reset ingredient calories to their original values
    public void ResetCalories()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients[i].Calories = originalCalories[i];
        }
    }

    // Method to clear the recipe
    public void ClearRecipe()
    {
        Ingredients.Clear();
        Steps.Clear();
        originalQuantities.Clear();
        originalCalories.Clear(); // Clear the list of original calorie values
    }

    // Helper method to read an integer value from the console
    private int ReadInt(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Invalid input. " + prompt);
        }
        return value;
    }

    // Helper method to read a double value from the console, with optional validation
    private double ReadDouble(string prompt, double[] validValues = null)
    {
        double value;
        Console.Write(prompt);
        while (!double.TryParse(Console.ReadLine(), out value) || (validValues != null && !validValues.Contains(value)))
        {
            Console.Write("Invalid input. " + prompt);
        }
        return value;
    }

    // Helper method to read a string value from the console
    private string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    // Method to notify if the total calories exceed 300
    public void NotifyIfCaloriesExceedLimit()
    {
        double totalCalories = CalculateTotalCalories();
        if (totalCalories > 300)
        {
            CalorieWarning?.Invoke($"Warning: Total calories of {Name} exceed 300!");
        }
    }
}
