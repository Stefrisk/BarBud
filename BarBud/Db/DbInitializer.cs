using BarBud.Models;

namespace BarBud.Db;

public static class DbInitializer
{
    public static void Seed(BarBudDbContext context)
    {
        // If any cocktails exist, assume already seeded
        if (context.Cocktails.Any())
        {
            return;
        }

        var cocktails = new List<Cocktail>
        {
            new Cocktail
            {
                Name = "Old Fashioned",
                Description = "A classic whiskey cocktail.",
                Recipe = new Recipe
                {
                    Name = "Old Fashioned Recipe",
                    Instructions = "Muddle sugar with bitters, add whiskey and ice, stir, garnish with orange peel.",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "Bourbon", Amount = "2 oz" },
                        new Ingredient { Name = "Angostura Bitters", Amount = "2 dashes" },
                        new Ingredient { Name = "Sugar Cube", Amount = "1 cube" },
                        new Ingredient { Name = "Orange Peel", Amount = "Garnish" }
                    }
                }
            },
            new Cocktail
            {
                Name = "Margarita",
                Description = "Refreshing tequila, lime, and triple sec.",
                Recipe = new Recipe
                {
                    Name = "Margarita Recipe",
                    Instructions = "Shake tequila, lime juice, and triple sec with ice, strain into salt-rimmed glass.",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "Tequila", Amount = "2 oz" },
                        new Ingredient { Name = "Lime Juice", Amount = "1 oz" },
                        new Ingredient { Name = "Triple Sec", Amount = "1 oz" },
                        new Ingredient { Name = "Salt", Amount = "Rim" }
                    }
                }
            },
            new Cocktail
            {
                Name = "Negroni",
                Description = "Equal parts gin, Campari, and sweet vermouth.",
                Recipe = new Recipe
                {
                    Name = "Negroni Recipe",
                    Instructions = "Stir ingredients with ice, strain, garnish with orange slice.",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "Gin", Amount = "1 oz" },
                        new Ingredient { Name = "Campari", Amount = "1 oz" },
                        new Ingredient { Name = "Sweet Vermouth", Amount = "1 oz" },
                        new Ingredient { Name = "Orange Slice", Amount = "Garnish" }
                    }
                }
            },
            new Cocktail
            {
                Name = "Mojito",
                Description = "Minty, lime, and rum highball.",
                Recipe = new Recipe
                {
                    Name = "Mojito Recipe",
                    Instructions = "Muddle mint with sugar and lime, add rum and soda, gently stir.",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "White Rum", Amount = "2 oz" },
                        new Ingredient { Name = "Fresh Mint", Amount = "8 leaves" },
                        new Ingredient { Name = "Lime Juice", Amount = "1 oz" },
                        new Ingredient { Name = "Sugar", Amount = "2 tsp" },
                        new Ingredient { Name = "Soda Water", Amount = "Top" }
                    }
                }
            },
            new Cocktail
            {
                Name = "Whiskey Sour",
                Description = "Balanced sweet and sour whiskey cocktail.",
                Recipe = new Recipe
                {
                    Name = "Whiskey Sour Recipe",
                    Instructions = "Shake whiskey, lemon, and syrup with ice, strain, optionally add egg white.",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "Bourbon", Amount = "2 oz" },
                        new Ingredient { Name = "Lemon Juice", Amount = "3/4 oz" },
                        new Ingredient { Name = "Simple Syrup", Amount = "1/2 oz" },
                        new Ingredient { Name = "Egg White", Amount = "Optional" }
                    }
                }
            }
        };

        context.Cocktails.AddRange(cocktails);
        context.SaveChanges();
    }
}