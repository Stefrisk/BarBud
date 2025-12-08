using BarBud.Models;
using BarBud.Db;
using Microsoft.EntityFrameworkCore;
using BarBud.Interfaces;

namespace BarBud.Services;

public class RecipeFunctions : IRecipeFunctions
{
    private readonly BarBudDbContext _dbContext;

    public RecipeFunctions(BarBudDbContext db)
    {
        _dbContext = db;
    }

    public async Task<Recipe> AddAsync(Recipe recipe)
    {
        _dbContext.Recipes.Add(recipe);
        await _dbContext.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        return await _dbContext.Recipes
            .Include(r => r.Ingredients)
                .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}
