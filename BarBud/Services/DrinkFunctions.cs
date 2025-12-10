using BarBud.Db;
using BarBud.Interfaces;
using BarBud.Models;
using Microsoft.EntityFrameworkCore;
namespace BarBud.Services;

public class DrinkFunctions : IDrinkServices
{
    private readonly BarBudDbContext _dbContext;

    public DrinkFunctions(BarBudDbContext db)
    {
        _dbContext = db;
    }
    public async Task<List<Drink>> GetAllDrinksAsync()
    {
        return await _dbContext.Drinks.ToListAsync();
    }
    public async Task<Drink?> GetByIdAsync(int id)
    {
        return await _dbContext.Drinks.FindAsync(id);
    }
    public async Task<Drink?> GetDetailsByIdAsync(int id)
    {
        return await _dbContext.Drinks
            .Include(d => d.Recipes)
                .ThenInclude(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Drink>> GetAllDrinksWithDetailsAsync(int userId)
    {
        return await _dbContext.Drinks
            .Where(d => d.TempUserID == userId)
            .Include(d => d.Recipes)
            .ThenInclude(r => r.Ingredients)
            .ThenInclude(ri => ri.Ingredient)
            .ToListAsync();
    }
    public async Task<Drink?> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        return await _dbContext.Drinks.FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task<Drink> AddAsync(Drink drink)
    {
        _dbContext.Drinks.Add(drink);
        await _dbContext.SaveChangesAsync();
        return drink;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var drink = await _dbContext.Drinks.FindAsync(id);
        if (drink == null) return false;

        _dbContext.Drinks.Remove(drink);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateAsync(Drink drink)
    {
        var exists = await _dbContext.Drinks.AnyAsync(c => c.Id == drink.Id);
        if (!exists) return false;
        _dbContext.Drinks.Update(drink);
        await _dbContext.SaveChangesAsync();
        return true;
    }
   

}
