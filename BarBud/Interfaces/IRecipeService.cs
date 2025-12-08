using BarBud.Models;

namespace BarBud.Interfaces;
public interface IRecipeFunctions
{
    Task<Recipe> AddAsync(Recipe recipe);
    Task<Recipe?> GetByIdAsync(int id);
}