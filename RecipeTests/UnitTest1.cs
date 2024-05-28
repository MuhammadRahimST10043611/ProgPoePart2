using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgPoePart2; // Make sure to include the namespace where your Recipe class resides

[TestClass]
public class RecipeTests
{
    [TestMethod]
    public void TestCalculateTotalCalories()
    {
        // Arrange
        Recipe recipe = new Recipe();
        recipe.Ingredients.Add(new Ingredient { Name = "Ingredient1", Calories = 100 });
        recipe.Ingredients.Add(new Ingredient { Name = "Ingredient2", Calories = 150 });
        recipe.Ingredients.Add(new Ingredient { Name = "Ingredient3", Calories = 200 });

        // Act
        double totalCalories = recipe.CalculateTotalCalories();

        // Assert
        Assert.AreEqual(450, totalCalories);
    }
}
