using CarReview.Models;
using Microsoft.EntityFrameworkCore;

namespace CarReview.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Profil> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarCategory>()
                .HasKey(cc => new { cc.CarId, cc.CategoryId });
            modelBuilder.Entity<CarCategory>()
                .HasOne(c => c.Car)
                .WithMany(cc => cc.CarCategories)
                .HasForeignKey(c => c.CarId);
            modelBuilder.Entity<CarCategory>()
                .HasOne(c => c.Category)
                .WithMany(cc => cc.CarCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Profil>()
                .HasOne(p => p.Reviewer)
                .WithOne(r => r.Profile)
                .HasForeignKey<Reviewer>(r => r.ProfileId);

        }
    }
}
