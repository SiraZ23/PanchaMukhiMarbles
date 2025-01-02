using Microsoft.EntityFrameworkCore;
using PanchaMukhiMarbles.API.Models.Domain;

namespace PanchaMukhiMarbles.API.Data
{
    public class PanchaMukhiMarblesDbContext:DbContext
    {
        public PanchaMukhiMarblesDbContext(DbContextOptions<PanchaMukhiMarblesDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed The Data For Categories
            //Tiles,Marbles,Sanitaries,Bathwares

            var categories = new List<Category>()
            {
                new Category()
                {
                    Id=Guid.Parse("29c77e11-70f2-4633-a16f-1b7d78c5ff20"),
                    Name="Tiles"
                },
                 new Category()
                 {
                     Id =Guid.Parse("c8c577b6-7787-4a4c-8ebb-3ed95531da72"),
                     Name = "Granites"
                 },
                  new Category()
                  {
                      Id = Guid.Parse("6da82802-aa65-4040-b937-84618c7d543e"),
                      Name = "Sanitaries"
                  },
                   new Category()
                   {
                       Id =Guid.Parse("fe707f6e-fc31-40b6-a6b3-fa2662ac92dc") ,
                       Name = "Bathwares"
                   }
            };

            //Seed Categories To The Db
            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
