namespace BarBud.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Cocktail> Cocktails { get; set; } = new();
    }

    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RecipeId { get; set; } // Foreign key for Recipe
        public Recipe Recipe { get; set; } = null!;
        public List<Tag> Tags { get; set; } = new();
    }

    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public string Amount { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }
}
