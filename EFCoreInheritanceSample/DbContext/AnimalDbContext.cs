using EFCoreInheritanceSample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInheritanceSample.DbContext;

public class AnimalDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Animals
        builder.Entity<Animal>()
            .ToTable("Animals");

        builder.Entity<Animal>()
            .HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(t => t.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Cats
        builder.Entity<Cat>()
            .ToTable("Cats");

        builder.Entity<Cat>()
            .HasBaseType<Animal>();

        // Dogs
        builder.Entity<Dog>()
            .ToTable("Dogs");

        builder.Entity<Dog>()
            .HasBaseType<Animal>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Server=localhost;Port=5432;Database=AnimalsDB;User Id=postgres;Password=1453;");
    }
}
