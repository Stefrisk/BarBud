using BarBud.Models;

namespace BarBud.Interfaces
{
    public interface IDrinkServices
    {
        public Task<List<Drink>> GetAllDrinksAsync();
        public Task<Drink> AddAsync(Drink drink);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(Drink drink);
    }
}