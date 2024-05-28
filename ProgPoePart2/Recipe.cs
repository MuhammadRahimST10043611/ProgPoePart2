using ProgPoePart2;
using System.Collections.Generic;
using System.Linq;
using System;

    class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<string> Steps { get; set; }
    private List<double> originalCalories;
    private List<string> originalQuantities;

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
        Steps = new List<string>();
        originalCalories = new List<double>();
        originalQuantities = new List<string>();
    }

    public void EnterRecipeDetails()
    {
        Console.Write("\nEnter recipe name: ");
        Name = Console.ReadLine();

        int numIngredients = ReadInt("Enter the number of ingredients: ");
        for (int i = 0; i < numIngredients; i++)
        {
            Console.WriteLine($"\nIngredient {i + 1}:");
            Ingredient ingredient = new Ingredient();
            ingredient.Name = ReadString("Enter ingredient name: ");
            ingredient.Quantity = ReadDouble("Enter quantity: ");
            ingredient.Unit = ReadString("Enter unit of measurement: ");
            ingredient.Calories = ReadDouble("Enter number of calories: ");
            ingredient.FoodGroup = ReadString("Enter food group: ");
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

    public double CalculateTotalCalories()
    {
        double totalCalories = 0;
        foreach (Ingredient ingredient in Ingredients)
        {
            totalCalories += ingredient.Calories;
        }
        return totalCalories;
    }

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

    public void ResetQuantities()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            string[] parts = originalQuantities[i].Split(' ');
            double quantity;
            if (double.TryParse(parts[0], out quantity))
            {
                Ingredients[i].Quantity = quantity;
            }
            else
            {
                Console.WriteLine($"Failed to parse quantity for ingredient: {originalQuantities[i]}");
            }
        }
    }

    public void ResetCalories()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients[i].Calories = originalCalories[i];
        }
    }

    public void ClearRecipe()
    {
        Ingredients.Clear();
        Steps.Clear();
        originalQuantities.Clear();
        originalCalories.Clear(); // Clear the list of original calorie values
    }

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

    private string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
