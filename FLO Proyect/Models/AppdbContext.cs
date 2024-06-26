﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FLO_Proyect.Models
{
    public class AppdbContext : IdentityDbContext<ProgramUser>
    {
        public AppdbContext(DbContextOptions<AppdbContext> options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<SizeToProduct> SizeToProduct { get; set; }
        public DbSet<ColorToProduct> ColorToProduct { get; set; }

        public DbSet<Images> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Orders>().HasData(
                new Orders { Id = 1, ProductId = 1, UserId = 1 },
                new Orders { Id = 2, ProductId = 2, UserId = 1 },
                new Orders { Id = 3, ProductId = 4, UserId = 2 }

                );
        }




    }
}
