using BarBud.Db;
using BarBud.Interfaces;
using BarBud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace BarBud.Services;

public class DrinkFunctions : IDrinkServices
{
    private readonly BarBudDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DrinkFunctions(BarBudDbContext db, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = db;
        _httpContextAccessor = httpContextAccessor;
    }
    public DrinkFunctions(BarBudDbContext db)
    {
        _dbContext = db;
        _httpContextAccessor = null;
    }
    private string? CurrentUserId => _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public async Task<List<Drink>> GetAllDrinksAsync()
    {
        var userId = CurrentUserId;
        if (userId is null)
        {
            return await _dbContext.Drinks.ToListAsync();
        }
        return await _dbContext.Drinks.Where(d => d.UserId == userId).ToListAsync();
    }
    public async Task<Drink?> GetByIdAsync(int id)
    {
        return await _dbContext.Drinks.FindAsync(id);
    }
    public async Task<Drink?> GetDetailsByIdAsync(int id)
    {
        var userId = CurrentUserId;
        var query = _dbContext.Drinks.Include(d => d.Recipes).ThenInclude(r => r.Ingredients).ThenInclude(ri => ri.Ingredient).AsQueryable();
      if(userId is not null)
        {
            query = query.Where(d => d.UserId == userId);
        }
        return await query.FirstOrDefaultAsync(d => d.Id == id);
    }
    public async Task<Drink?> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        return await _dbContext.Drinks.FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task<List<Drink>> AddAsync(Drink drink)
    {
        var userId = CurrentUserId;
        if (userId is not null)
        {
            drink.UserId = userId; 
        }
        _dbContext.Drinks.Add(drink);
        await _dbContext.SaveChangesAsync();
        return await GetAllDrinksAsync();
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var userId = CurrentUserId;
        var query = _dbContext.Drinks.AsQueryable();
        if (userId is not null)
        {
            query = query.Where(d => d.UserId == userId);
        }
        var drink = await query.FirstOrDefaultAsync(d => d.Id == id);
        if (drink == null) return false;
        _dbContext.Drinks.Remove(drink);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateAsync(Drink drink)
    {
        var userId = CurrentUserId;
        var query = _dbContext.Drinks.AsQueryable();
        if (userId is not null)
        {
            query = query.Where(d => d.UserId == userId);
        }
        var exists = await query.AnyAsync(d => d.Id == drink.Id);
        if (!exists) return false;
        if (userId is not null)
        {
            drink.UserId = userId; 
        }
        _dbContext.Drinks.Update(drink);
        await _dbContext.SaveChangesAsync();
        return true;
    }
   

}
