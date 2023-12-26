using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recipe.API.Models;

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
            }
        }
    }
}
