using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<City> Cities{ get; set; }
    public DbSet<District> Districts{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);// <== 這句啟用自動刪除 Profile

        modelBuilder.Entity<City>()
            .HasMany(c => c.Districts)
            .WithOne(d => d.City)
            .HasForeignKey(d => d.CityId);

        modelBuilder.Entity<District>()
            .HasMany(d => d.Profiles)
            .WithOne(r => r.District)
            .HasForeignKey(r => r.DistrictId);


    }
}
