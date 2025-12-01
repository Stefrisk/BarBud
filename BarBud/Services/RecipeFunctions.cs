using BarBud.Models;
using BarBud.Db;
using Microsoft.EntityFrameworkCore;

namespace BarBud.Services
{
    public class RecipeFunctions
    {
        private readonly BarBudDbContext _dbContext;

        public RecipeFunctions(BarBudDbContext db)
        {
            _dbContext = db;
        }
       
    }
}
