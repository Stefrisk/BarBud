using BarBud.Models;

namespace BarBud.Interfaces;

public interface IIngredientServices
{
    public Task<List<Ingredient>> GetAllIngredientsAsync();
    public Task<Ingredient?> AddAsync(Ingredient ingredient);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> UpdateAsync(Ingredient ingredient);
}