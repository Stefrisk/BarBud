using System.ComponentModel.DataAnnotations;
namespace BarBud.Models
{
    public class Drink
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        public string? Description { get; set; } 
        public Recipe Recipe { get; set; } 
    }
}