namespace Recipe.API.Models
{
    public enum Difficulty
    {
        Easy,
        Intermediate,
        Advanced
    }
    public class Recipes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Time { get; set; } // In minuten
        public Difficulty Difficulty { get; set; }
        public int CategoryId { get; set; }

        public Categories Category { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public string GetFormattedTime()
        {
            int hours = Time / 60;
            int minutes = Time % 60;
            return $"{minutes} minuten";
        }

    }


}
