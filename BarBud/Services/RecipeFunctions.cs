using BarBud.Db;
using BarBud.Interfaces;
using BarBud.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarBud.Services;

public class RecipeFunctions : IRecipeFunctions
{
    private readonly BarBudDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor; 

    public RecipeFunctions(BarBudDbContext db, IHttpContextAccessor httpContextAccessor) 
    {
        _dbContext = db;
        _httpContextAccessor = httpContextAccessor;
    }

    
    private string? CurrentUserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
            

    public async Task<Recipe> AddAsync(Recipe recipe)
    {
        var userId = CurrentUserId;

        if (userId is not null)
        {
            recipe.UserId = userId;
        }
        _dbContext.Recipes.Add(recipe);
        await _dbContext.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        var userId = CurrentUserId;

        var query = _dbContext.Recipes
            .Include(r => r.Ingredients)
                .ThenInclude(ri => ri.Ingredient)
            .AsQueryable();

        if (userId is not null)
        {           
            query = query.Where(r => r.UserId == userId);
        }

        return await query.FirstOrDefaultAsync(r => r.Id == id);
    }
}
