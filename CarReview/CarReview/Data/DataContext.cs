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

        //public DbSet<Profil> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        //trb scos reviewer
        //public DbSet<Reviewer> Reviewers { get; set; }
        //trb scos reviewer
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Review>()
                .HasKey(u => u.Id);

            //O-O
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserDetails)
                .WithOne(u => u.User)
                .HasForeignKey<UserDetails>(u => u.UserId);

            //O-M (car-review, user-review) 
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Car)
                .HasForeignKey(r => r.CarId);
            modelBuilder.Entity<User>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);


            //M-M
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



        }
    }
}
