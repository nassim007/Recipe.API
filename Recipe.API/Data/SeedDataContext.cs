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
            }
        }
    }
}
