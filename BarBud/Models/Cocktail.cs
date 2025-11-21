namespace BarBud.Models
{
    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Recipe Recipe { get; set; } = null!;
    }
}