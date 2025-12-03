using BarBud.Models;

namespace BarBud.Db;

public static class DbInitializer
{
    public static void Seed(BarBudDbContext context)
    {
        // If any drinks exist, assume already seeded
        if (context.Drinks.Any())
        {
            return;
        }

        var drinks = new List<Drink>
        {
            new Drink
            {
                Name = "Old Fashioned",
                Description = "A classic whiskey cocktail.",
                Recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Name = "Old Fashioned Recipe",
                        Instructions = "Muddle sugar with bitters, add whiskey and ice, stir, garnish with orange peel.",
                        Ingredients = new List<RecipeIngredient>
                        {
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Bourbon"}, Quantity = 2, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Angostura Bitters" }, Quantity = 2, Unit = "dashes" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Sugar Cube" }, Quantity = 1, Unit = "cube" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Orange Peel" }, Quantity = 1, Unit = "garnish"  }
                        },
                        Drink = null! // Will be set below
                    }
                }
            },
            new Drink
            {
                Name = "Margarita",
                Description = "Refreshing tequila, lime, and triple sec.",
                Recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Name = "Margarita Recipe",
                        Instructions = "Shake tequila, lime juice, and triple sec with ice, strain into salt-rimmed glass.",
                        Ingredients = new List<RecipeIngredient>
                        {
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Tequila" }, Quantity = 2, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Lime Juice" }, Quantity = 1, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Triple Sec" }, Quantity = 1, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Salt" }, Quantity = 1, Unit = "Rim" }
                        },
                        Drink = null!
                    }
                }
            },
            new Drink
            {
                Name = "Negroni",
                Description = "Equal parts gin, Campari, and sweet vermouth.",
                Recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Name = "Negroni Recipe",
                        Instructions = "Stir ingredients with ice, strain, garnish with orange slice.",
                        Ingredients = new List<RecipeIngredient>
                        {
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Gin" }, Quantity = 1, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Campari" }, Quantity = 1, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Sweet Vermouth" }, Quantity = 1, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Orange Slice" }, Quantity = 1, Unit = "garnish" }
                        },
                        Drink = null!
                    }
                }
            },
            new Drink
            {
                Name = "Mojito",
                Description = "Minty, lime, and rum highball.",
                Recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Name = "Mojito Recipe",
                        Instructions = "Muddle mint with sugar and lime, add rum and soda, gently stir.",
                        Ingredients = new List<RecipeIngredient>
                        {
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "White Rum" }, Quantity = 2, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Fresh Mint" }, Quantity = 8, Unit = "leaves" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Lime Juice" }, Quantity = 1, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Sugar" }, Quantity = 2, Unit = "tsp" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Soda Water" }, Quantity = 1, Unit = "Top" }
                        },
                        Drink = null!
                    }
                }
            },
            new Drink
            {
                Name = "Whiskey Sour",
                Description = "Balanced sweet and sour whiskey cocktail.",
                Recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Name = "Whiskey Sour Recipe",
                        Instructions = "Shake whiskey, lemon, and syrup with ice, strain, optionally add egg white.",
                        Ingredients = new List<RecipeIngredient>
                        {
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Bourbon" }, Quantity = 2, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Lemon Juice" }, Quantity = 0.75m, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Simple Syrup" }, Quantity = 0.5m, Unit = "oz" },
                            new RecipeIngredient { Ingredient = new Ingredient { Name = "Egg White" }, Quantity = 1, Unit = "Optional" }
                        },
                        Drink = null!
                    }
                }
            }
        };

        // Set the Drink property for each Recipe
        foreach (var drink in drinks)
        {
            if (drink.Recipes != null)
            {
                foreach (var recipe in drink.Recipes)
                {
                    recipe.Drink = drink;
                }
            }
        }

        context.Drinks.AddRange(drinks);
        context.SaveChanges();
    }
}