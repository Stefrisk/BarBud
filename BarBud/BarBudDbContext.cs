using Microsoft.EntityFrameworkCore;
using BarBud.Models;

namespace BarBud
{
    public class BarBudDbContext : DbContext
    {
        public BarBudDbContext(DbContextOptions<BarBudDbContext> options) : base(options)
        {
        }

        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Cocktail>()
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