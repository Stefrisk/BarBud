using BarBud.Models;

namespace BarBud.Interfaces;
public interface IRecipeServices
{
    Task<Recipe> AddAsync(Recipe recipe);
    Task<Recipe?> GetByIdAsync(int id);
}