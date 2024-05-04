using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FLO_Proyect.Models
{
    public class AppdbContext: IdentityDbContext<ProgramUser>
    {
        public AppdbContext(DbContextOptions options) :base(options) 
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
        public List<Orders> Orders { get; set; }

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
