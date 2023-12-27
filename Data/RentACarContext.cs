using rent_a_car_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace rent_a_car_mvc.Data
{
    public class RentACarContext : DbContext
    {
        static readonly string connectionString = "Server=localhost;Database=Seng311RentACarDB;User Id=sa;Password=12345OHdf%e;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Insert the list of employees into the "Employees" table
            modelBuilder.Entity<Car>().HasData(
                new()
                {
                    Id = 1,
                    Make = "Aston Martin",
                    Model = "Vanquish",
                    RegistrationPlate = "AM0-V-001",
                },
                new()
                {
                    Id = 2,
                    Make = "Mercedes Benz",
                    Model = "S Class",
                    RegistrationPlate = "MB0-S-001",
                },
                new()
                {
                    Id = 3,
                    Make = "Rolls Royce",
                    Model = "Wraith",
                    RegistrationPlate = "RR0-W-001",
                }
            );
            modelBuilder.Entity<Customer>().HasData(
                new()
                {
                    Id = 1,
                    Name = "Ibrahim Sefer",
                    Email = "seferibrahim2@gmail.com",
                    Telephone = "+905363344840",
                    DateOfBirth = new DateTime(2002, 1, 31),
                },
                new()
                {
                    Id = 2,
                    Name = "John Smith",
                    Email = "johnsmith@gmail.com",
                    Telephone = "+905361123440",
                    DateOfBirth = new DateTime(2001, 2, 21),
                },
                new()
                {
                    Id = 3,
                    Name = "Mark Markovich",
                    Email = "markmarkovich@gmail.com",
                    Telephone = "+905369977660",
                    DateOfBirth = new DateTime(2000, 3, 11),
                }
            );
            modelBuilder.Entity<Reservation>().HasData(
                new()
                {
                    Id = 1,
                    StartLocation = "Ankara Airport",
                    StartDateTime = new DateTime(2023, 12, 20, 10, 0, 0),
                    EndLocation = "Ankara Airport",
                    EndDateTime = new DateTime(2023, 12, 25, 10, 0, 0),
                    TotalAmount = 10000,
                    Deposit = 4000,
                    FlightNumber = "TK1002",
                    CarId = 1,
                    RenterId = 1
                },
                new()
                {
                    Id = 2,
                    StartLocation = "Ankara Airport",
                    StartDateTime = new DateTime(2023, 12, 20, 10, 0, 0),
                    EndLocation = "Ankara Airport",
                    EndDateTime = new DateTime(2023, 12, 25, 10, 0, 0),
                    TotalAmount = 20000,
                    Deposit = 8000,
                    FlightNumber = "TK1005",
                    CarId = 2,
                    RenterId = 2
                },
                new()
                {
                    Id = 3,
                    StartLocation = "Ankara Airport",
                    StartDateTime = new DateTime(2023, 12, 30, 10, 0, 0),
                    EndLocation = "Ankara Airport",
                    EndDateTime = new DateTime(2024, 1, 4, 10, 0, 0),
                    TotalAmount = 30000,
                    Deposit = 8000,
                    FlightNumber = "TK1008",
                    CarId = 2,
                    RenterId = 2
                }
            );

            modelBuilder.Entity<Contract>().HasData(
                new()
                {
                    Id = 1,
                    StartLocation = "Ankara Airport",
                    StartDateTime = new DateTime(2023, 12, 20, 10, 0, 0),
                    EndLocation = "Ankara Airport",
                    EndDateTime = new DateTime(2023, 12, 25, 10, 0, 0),
                    TotalAmount = 60000,
                    Deposit = 4000,
                    CarId = 1,
                    RenterId = 1,
                    DriverId = 1,
                },
                new()
                {
                    Id = 2,
                    StartLocation = "Ankara Airport",
                    StartDateTime = new DateTime(2023, 12, 20, 10, 0, 0),
                    EndLocation = "Ankara Airport",
                    EndDateTime = new DateTime(2023, 12, 25, 10, 0, 0),
                    TotalAmount = 50000,
                    Deposit = 8000,
                    CarId = 2,
                    RenterId = 2,
                    DriverId = 1,
                },
                new()
                {
                    Id = 3,
                    StartLocation = "Ankara Airport",
                    StartDateTime = new DateTime(2023, 12, 30, 10, 0, 0),
                    EndLocation = "Ankara Airport",
                    EndDateTime = new DateTime(2024, 1, 4, 10, 0, 0),
                    TotalAmount = 50000,
                    Deposit = 8000,
                    CarId = 2,
                    RenterId = 2,
                    DriverId = 2,
                }
            );

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Renter)
                .WithMany()
                .HasForeignKey(c => c.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Driver)
                .WithMany()
                .HasForeignKey(c => c.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}