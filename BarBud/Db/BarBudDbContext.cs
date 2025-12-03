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
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Drink)
                .WithMany(d => d.Recipes)
                .HasForeignKey(r => r.DrinkId);

            modelBuilder.Entity<RecipeIngredient>().
                HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>().
                HasOne(ri => ri.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(ri => ri.RecipeId).
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeIngredient>().
                HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId).
                OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // Property Constraints
            // -----------------------------
            modelBuilder.Entity<Drink>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<RecipeIngredient>()
                .Property(ri => ri.Quantity)
                .IsRequired()
                .HasPrecision(10, 2); // Avoids float garbage
        }
    }
}