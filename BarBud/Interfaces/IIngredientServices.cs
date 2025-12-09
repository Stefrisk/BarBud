using BarBud.Models;

namespace BarBud.Interfaces;

public interface IIngredientServices
{
    public Task<List<Ingredient>> GetAllIngredientsAsync();
    public Task<List<Ingredient>> AddAsync(Ingredient ingredient);
    public Task<Boolean> DeleteAsync(int id);
    public Task<Boolean> UpdateAsync(Ingredient ingredient);
}