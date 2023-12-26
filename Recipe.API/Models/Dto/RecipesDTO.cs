namespace Recipe.API.Models.Dto
{
    public class RecipesDTO
    {
        public class RecipesBasic
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Time { get; set; }
            public string Category { get; set; }
            public Difficulty Difficulty { get; set; }
        }

        public class RecipesDetail
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Time { get; set; }
            public string Category { get; set; }
            public Difficulty Difficulty { get; set; }
            public List<IngredientDTO> Ingredients { get; set; }
        }

        public class IngredientDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Quantity { get; set; }
            public string Unit { get; set; }
        }
    }
}
