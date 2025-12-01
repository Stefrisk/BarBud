using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BarBud.Db
{
    // Design-time factory so `dotnet ef` can create the context outside of the running app.
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BarBudDbContext>
    {
        public BarBudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BarBudDbContext>();

            // Use the same connection string as Program.cs
            builder.UseSqlite("Data Source=barbud.db");

            return new BarBudDbContext(builder.Options);
        }
    }
}
