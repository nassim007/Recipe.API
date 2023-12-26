namespace Recipe.API.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public int RecipesId { get; set; }

        public Recipes Recipes { get; set; }
    }
}
