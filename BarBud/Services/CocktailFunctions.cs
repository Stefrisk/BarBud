using BarBud.Db;
using BarBud.Models;
using Microsoft.EntityFrameworkCore;
namespace BarBud.Services
{
    public class CocktailFunctions
    {
        private readonly BarBudDbContext _dbContext;

        public CocktailFunctions(BarBudDbContext db)
        {
            _dbContext = db;
        }
        public async Task<List<Cocktail>> GetAllCocktailsAsync()
        {
            return await _dbContext.Cocktails.ToListAsync();
        }
        public async Task<Cocktail?> GetByIdAsync(int id)
        {
            return await _dbContext.Cocktails.FindAsync(id);
        }
        public async Task<Cocktail?> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            return await _dbContext.Cocktails.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<Cocktail> AddAsync(Cocktail cocktail)
        {
            _dbContext.Cocktails.Add(cocktail);
            await _dbContext.SaveChangesAsync();
            return cocktail;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var cocktail = await _dbContext.Cocktails.FindAsync(id);
            if (cocktail == null) return false;

            _dbContext.Cocktails.Remove(cocktail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(Cocktail cocktail)
        {
            var exists = await _dbContext.Cocktails.AnyAsync(c => c.Id == cocktail.Id);
            if (!exists) return false;
            _dbContext.Cocktails.Update(cocktail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
       

    }
}
