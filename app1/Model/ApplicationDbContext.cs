using Microsoft.EntityFrameworkCore;

namespace app1.Model
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {}
        public DbSet<VillaModel> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<LocalUserModel> localUser { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VillaModel>().HasData(

                new VillaModel
                {
                    Id = 1,
                    Name = "Luxury Villa",
                    Description = "Experience luxury living in this beautiful villa, complete with private pool and stunning views of the ocean.",
                    Rate = 500.00,
                    sqft = 3000,
                    ImageUrl = "https://example.com/luxury-villa.jpg",
                    CreateData = DateTime.Now,
                    UpdateData = DateTime.Now
                },
                new VillaModel
                {
                    Id = 2,
                    Name = "Beachfront Villa",
                    Description = "Relax and unwind in this beachfront villa, just steps away from the pristine white sands of the beach.",
                    Rate = 350.00,
                    sqft = 2000,
                    ImageUrl = "https://example.com/beachfront-villa.jpg",
                    CreateData = DateTime.Now,
                    UpdateData = DateTime.Now
                },
                new VillaModel
                {
                    Id = 3,
                    Name = "Tropical Paradise Villa",
                    Description = "Escape to this tropical paradise villa, surrounded by lush greenery and a serene waterfall.",
                    Rate = 450.00,
                    sqft = 2500,
                    ImageUrl = "https://example.com/tropical-paradise-villa.jpg",
                    CreateData = DateTime.Now,
                    UpdateData = DateTime.Now
                });
        }
        
    }
}
