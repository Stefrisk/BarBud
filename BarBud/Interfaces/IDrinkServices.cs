using BarBud.Models;

namespace BarBud.Interfaces;

public interface IDrinkServices
{
    Task<Drink> AddAsync(Drink drink);
    Task<bool> DeleteAsync(int id);
    Task<List<Drink>> GetAllDrinksAsync();
    Task<Drink?> GetByIdAsync(int id);
    Task<Drink?> GetByNameAsync(string name);
    Task<Drink?> GetDetailsByIdAsync(int id);
    Task<bool> UpdateAsync(Drink drink);
}