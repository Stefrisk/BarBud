using System.ComponentModel.DataAnnotations;
namespace BarBud.Models;

public class Drink
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    //User 
    public string UserId { get; set; } = default!;

    public User user { get; set; } = default!;
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}