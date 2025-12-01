using BarBud.Models;
using BarBud.Db;
using Microsoft.EntityFrameworkCore;

namespace BarBud.Services
{
    public class RecipeServices
    {
        private readonly BarBudDbContext _dbContext;

        public RecipeServices(BarBudDbContext db)
        {
            _dbContext = db;
        }
       
    }
}
