using BarBud.Db;
using BarBud.Interfaces;
using BarBud.Models;
using Microsoft.EntityFrameworkCore;
namespace BarBud.Services;

public class IngredientFunctions : IIngredientServices
{
    private readonly BarBudDbContext _dbContext;
    public IngredientFunctions(BarBudDbContext db)
    {
        _dbContext = db;
    }
    public async Task<List<Ingredient>> GetAllIngredientsAsync()
    {
        return await _dbContext.Ingredients.ToListAsync();
    }
    /*public async Task<Ingredient?> GetByIdAsync(int id)
    {
        return await _dbContext.Ingredients.FindAsync(id);
    }
    public async Task<Ingredient?> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        return await _dbContext.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
    }*/
    public async Task<List<Ingredient>> AddAsync(Ingredient ingredient)
    {
        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();
        
        return await GetAllIngredientsAsync();
    }
    public async Task<bool> DeleteAsync(int id) 
    {
        var ingredient = await _dbContext.Ingredients.FindAsync(id);
        if (ingredient == null) return false;

        _dbContext.Ingredients.Remove(ingredient);
        await _dbContext.SaveChangesAsync();
        return true;

    }
    public async Task<bool> UpdateAsync(Ingredient ingredient)
    {
      var exists = await _dbContext.Ingredients.AnyAsync(i => i.Id == ingredient.Id);
        if (!exists) return false;
        _dbContext.Ingredients.Update(ingredient);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}