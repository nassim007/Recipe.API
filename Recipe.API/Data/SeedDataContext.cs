using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recipe.API.Models;
using System;

namespace Recipe.API.Data
{
    public static class SeedDataInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MyDbContext>>()))
            {
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Categories { Name = "Soepen" },
                        new Categories { Name = "Vegetarisch" },
                        new Categories { Name = "Voorgerecht" },
                        new Categories { Name = "Hoofdgerecht" },
                        new Categories { Name = "Dessert" }
                    );

                    context.SaveChanges();
                }

                if (!context.Recipes.Any())
                {
                    // Add recipes with specific characteristics based on search options
                    context.Recipes.AddRange(
                        new Recipes { Title = "Recipe 1", Time = 45, Difficulty = Difficulty.Intermediate, CategoryId = 7 },
                        new Recipes { Title = "Recipe 2", Time = 30, Difficulty = Difficulty.Easy, CategoryId = 8 }
                        // Add more recipes as needed
                    );

                    context.SaveChanges();
                }
                if (!context.Ingredients.Any())
                {
                    // Assuming that RecipesId values correspond to existing recipe IDs
                    context.Ingredients.AddRange(
                        new Ingredient { Name = "Ingredient 1", Quantity = 2, Unit = "cups", RecipesId = 2 },
                        new Ingredient { Name = "Ingredient 2", Quantity = 500, Unit = "g", RecipesId = 3 }
                    // Add more ingredients as needed
                    );

                    context.SaveChanges();
                }

                if (!context.SignUpModels.Any())
                {
                    // Voeg gebruikersgegevens toe
                    context.SignUpModels.AddRange(
                        new SignUpModel
                        {
                            FirstName = "User",
                            LastName = "User2",
                            Username = "User",
                            Email = "user@example.com",
                            Password = "1234",
                            ConfirmPassword = "1234",
                            IsAdmin = false
                        },
                        new SignUpModel
                        {
                            FirstName = "Admin",
                            LastName = "Admin",
                            Username = "Admin",
                            Email = "Admin@example.com",
                            Password = "1234",
                            ConfirmPassword = "1234",
                            IsAdmin = true
                        }
                    );

                    context.SaveChanges();
                }
                if (!context.SignInModels.Any())
                {
                    var signInModel1 = new SignInModel { Username = "user1", Password = "12345" };
                    var signInModel2 = new SignInModel { Username = "user2", Password = "12345" };

                    context.SignInModels.AddRange(signInModel1, signInModel2);
                    context.SaveChanges();
                }
            }
        }
    }
}
