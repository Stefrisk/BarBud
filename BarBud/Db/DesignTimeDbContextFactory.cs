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

            // Use the project-local SQLite file. This keeps the EF tools working without needing appsettings.
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "barbud.db");
            var conn = $"Data Source={dbPath}";

            builder.UseSqlite(conn);

            return new BarBudDbContext(builder.Options);
        }
    }
}
