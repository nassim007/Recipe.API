namespace Recipe.API.Models
{
    public class RecipeSearchOptions
    {
        public string SearchTerm { get; set; }
        public List<int> Categories { get; set; }
        public int? MaxDifficulty { get; set; }
        public int? MaxTime { get; set; }
    }
}
