using BarBud.Models;

namespace BarBud.Interfaces;

public interface IIngredientServices
{
    public Task<List<Ingredient>> GetAllIngredientsAsync();
    public Task<List<Ingredient>> AddAsync(Ingredient ingredient);
    public Task<List<Ingredient>> DeleteAsync(int id);
    public Task<List<Ingredient>> UpdateAsync(Ingredient ingredient);
}