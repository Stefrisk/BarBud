using BarBud.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarBud.Db
{
    public class BarBudDbContext : IdentityDbContext<User>
    {
        public BarBudDbContext(DbContextOptions<BarBudDbContext> options) : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map Drink to the existing SQLite table name
            modelBuilder.Entity<Drink>().ToTable("Cocktails");

            modelBuilder.Entity<Drink>()
                .HasOne(c => c.Recipe)
                .WithOne()
                .HasForeignKey<Recipe>(r => r.Id);


            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne()
                .HasForeignKey("RecipeId");
        }
    }
}