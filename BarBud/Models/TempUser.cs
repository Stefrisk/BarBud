namespace BarBud.Models;

public class TempUser
{
    // identity and blazor components are not a great match so we will use this TempUser entity temporarily

    public int Id { get; set; } // this is our PK
    public string Name { get; set; }

    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public ICollection<Drink> Drinks { get; set; } = new List<Drink>();
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}