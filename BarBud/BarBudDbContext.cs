using Microsoft.EntityFrameworkCore;
using BarBud.Models;

namespace BarBud
{
    public class BarBudDbContext : DbContext
    {
        public BarBudDbContext(DbContextOptions<BarBudDbContext> options) : base(options) { }

        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            modelBuilder.Entity<Cocktail>()
                .HasMany(c => c.Tags)   
                .WithMany(t => t.Cocktails);
        }
    }
}
