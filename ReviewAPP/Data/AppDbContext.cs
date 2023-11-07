using Microsoft.EntityFrameworkCore;
using ReviewAPP.Models;

namespace ReviewAPP.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<Category> Categories { get; set; } 
        public DbSet<Place> Places { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Reviewer> Reviewers { get; set; }
        
        public DbSet<PlaceCategory> PlaceCategory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaceCategory>()
                    .HasKey(pc => new { pc.PlaceID, pc.CategoryID });
            modelBuilder.Entity<PlaceCategory>()
                    .HasOne(p => p.Place)
                    .WithMany(pc => pc.PlaceCategories)
                    .HasForeignKey(p => p.PlaceID);
            modelBuilder.Entity<PlaceCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.PlaceCategories)
                    .HasForeignKey(c => c.CategoryID);


        }


    }
}
