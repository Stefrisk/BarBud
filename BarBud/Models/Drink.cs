using System.ComponentModel.DataAnnotations;
namespace BarBud.Models;

public class Drink
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } 
    public string? Description { get; set; } = string.Empty;
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}