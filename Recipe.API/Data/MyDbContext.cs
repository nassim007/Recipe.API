using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipe.API.Models;
using Recipe.API.Data;


namespace Recipe.API.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            {

            }
            modelBuilder.Entity<Ingredient>()
           .Property(i => i.Quantity)
           .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed



        }
    }


    }

