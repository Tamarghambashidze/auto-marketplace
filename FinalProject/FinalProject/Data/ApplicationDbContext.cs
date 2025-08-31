using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
  public class ApplicationDbContext : DbContext
  {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarDetails> CarDetails { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<DriveTrain> DriveTrains { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<CarFavourite> CarFavourites { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)"); 


            // One - to - one relationships
            modelBuilder.Entity<Car>()
              .HasOne(c => c.Details)
              .WithOne(d => d.Car)
              .HasForeignKey<CarDetails>(d => d.CarId)
              .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
              .HasOne(u => u.UserDetails)
              .WithOne(ud => ud.User)
              .HasForeignKey<UserDetails>(ud => ud.UserId)
              .OnDelete(DeleteBehavior.Cascade); ;
            modelBuilder.Entity<User>()
              .HasOne(u => u.Favourites)
              .WithOne(f => f.User)
              .HasForeignKey<Favourites>(f => f.UserId)
              .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Transaction)
                .WithOne(t => t.BoughtCar)
                .HasForeignKey<Transaction>(t => t.BoughtCarId);

            // one-to-many relationships
            modelBuilder.Entity<Manufacturer>()
              .HasMany(m => m.Cars)
              .WithOne(c => c.Manufacturer)
              .HasForeignKey(c => c.ManufacturerId);
            modelBuilder.Entity<Transmission>()
              .HasMany(t => t.Cars)
              .WithOne(cd => cd.Transmission)
              .HasForeignKey(cd => cd.TransmissionId);
            modelBuilder.Entity<DriveTrain>()
              .HasMany(dt => dt.Cars)
              .WithOne(cd => cd.DriveTrain)
              .HasForeignKey(cd => cd.DrivetrainId);
            modelBuilder.Entity<FuelType>()
              .HasMany(ft => ft.Cars)
              .WithOne(cd => cd.FuelType)
              .HasForeignKey(cd => cd.FuelTypeId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);


            //many-to-many relationship
            modelBuilder.Entity<CarFavourite>().HasKey(cf => new { cf.CarId, cf.FavouriteId });

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Favourites)
                .WithMany(f => f.Cars)
                .UsingEntity<CarFavourite>(
                    j => j.HasOne(cf => cf.Favourites).WithMany().HasForeignKey(cf => cf.FavouriteId),
                    j => j.HasOne(cf => cf.Car).WithMany().HasForeignKey(cf => cf.CarId)
                );

            modelBuilder.Entity<User>()
                .HasOne(u => u.Favourites)
                .WithOne(f => f.User)
                .HasForeignKey<Favourites>(f => f.UserId);
        }
  }
}
