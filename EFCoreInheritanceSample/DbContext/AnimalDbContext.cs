using EFCoreInheritanceSample.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EFCoreInheritanceSample.DbContext;

public class AnimalDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPT (Table Per Type) konfigürasyonu
        //modelBuilder.Entity<Animal>()
        //    .UseTptMappingStrategy();

        // Alternatif olarak aşağıdaki şekilde de yapabilirsiniz:
        // modelBuilder.Entity<Cat>().ToTable("Cats");
        // modelBuilder.Entity<Dog>().ToTable("Dogs");

        // Self-referencing relationship konfigürasyonu
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Parent)
            .WithMany(a => a.Children)
            .HasForeignKey(a => a.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Animal base entity konfigürasyonu
        modelBuilder.Entity<Animal>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Animal>()
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Cat specific konfigürasyon
        modelBuilder.Entity<Cat>()
            .Property(c => c.MiceCaughtCount)
            .IsRequired();

        // Dog specific konfigürasyon
        modelBuilder.Entity<Dog>()
            .Property(d => d.BonesBuriedCount)
            .IsRequired();

        // Index'ler
        modelBuilder.Entity<Animal>()
            .HasIndex(a => a.ParentId)
            .HasDatabaseName("IX_Animals_ParentId");

        modelBuilder.Entity<Animal>()
            .HasIndex(a => a.Name)
            .HasDatabaseName("IX_Animals_Name");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Server=localhost;Port=5432;Database=AnimalsDB;User Id=postgres;Password=1453;");
    }
}


