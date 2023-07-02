namespace dotnet_rideShare.Contexts;

using Microsoft.EntityFrameworkCore;
using dotnet_rideShare.Entities;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Users = Set<Users>();
        Cities = Set<Cities>();
        PassengerPlans = Set<PassengerPlans>();
        TravelPlans = Set<TravelPlans>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PassengerPlans>()
            .Property(p => p.Status)
            .HasConversion<string>();
        modelBuilder.Entity<TravelPlans>()
            .Property(p => p.Status)
            .HasConversion<string>();
        modelBuilder.Entity<Users>()
            .Property(p => p.Role)
            .HasConversion<string>();
    }

    public DbSet<Users> Users { get; set; }

    public DbSet<Cities> Cities { get; set; }

    public DbSet<PassengerPlans> PassengerPlans { get; set; }

    public DbSet<TravelPlans> TravelPlans { get; set; }

}
