namespace BarBud.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}
