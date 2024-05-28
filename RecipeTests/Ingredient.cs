namespace ProgPoePart2
{
    /// <summary>
    /// Class to represent an ingredient.
    /// </summary>
    class Ingredient
    {
        // Properties to store ingredient details
        public string Name { get; set; }       // Name of the ingredient
        public double Quantity { get; set; }   // Quantity of the ingredient
        public string Unit { get; set; }       // Unit of measurement for the quantity
        public double Calories { get; set; }   // Calories per serving of the ingredient
        public string FoodGroup { get; set; }  // Food group to which the ingredient belongs
    }
}
