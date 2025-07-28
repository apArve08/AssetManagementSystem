using ITAssetManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed default users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123", // In production, this should be hashed
                    Role = "Admin",
                    FullName = "System Administrator",
                    Email = "admin@company.com"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Password = "user123", // In production, this should be hashed
                    Role = "User",
                    FullName = "Normal User",
                    Email = "user@company.com"
                }
            );

            // Seed sample assets
            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    Id = 1,
                    AssetTag = "IT-001",
                    Name = "Dell Laptop",
                    Category = "Laptop",
                    Brand = "Dell",
                    Model = "Latitude 5520",
                    SerialNumber = "DL5520001",
                    PurchaseDate = DateTime.UtcNow.AddMonths(-6),
                    PurchasePrice = 1200,
                    Status = "Assigned",
                    AssignedTo = "John Doe",
                    AssignedDate = DateTime.UtcNow.AddMonths(-5),
                    Location = "Floor 2, IT Dept"
                },
                new Asset
                {
                    Id = 2,
                    AssetTag = "IT-002",
                    Name = "HP Monitor",
                    Category = "Monitor",
                    Brand = "HP",
                    Model = "E24 G4",
                    SerialNumber = "HPE24001",
                    PurchaseDate = DateTime.UtcNow.AddMonths(-4),
                    PurchasePrice = 250,
                    Status = "Available",
                    Location = "Storage Room"
                },
                new Asset
                {
                    Id = 3,
                    AssetTag = "IT-003",
                    Name = "MacBook Pro",
                    Category = "Laptop",
                    Brand = "Apple",
                    Model = "MacBook Pro 14",
                    SerialNumber = "MB14001",
                    PurchaseDate = DateTime.UtcNow.AddMonths(-2),
                    PurchasePrice = 2500,
                    Status = "Repair",
                    Location = "IT Service Desk"
                }
            );
        }
    }
}