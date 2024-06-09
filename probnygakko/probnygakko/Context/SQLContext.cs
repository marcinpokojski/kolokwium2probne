using Microsoft.EntityFrameworkCore;
using probnygakko.Models;

namespace probnygakko.Context;

public class SQLContext : DbContext
{
    public SQLContext()
    {

    }

    public SQLContext(DbContextOptions<SQLContext> options)
        : base(options)
    {
    }

    public DbSet<Album> Albumy { get; set; }
    public DbSet<Muzyk> Muzycy { get; set; }
    public DbSet<Utwor> Utwory { get; set; }
    public DbSet<WykonawcaUtworu> Wykonawcy { get; set; }
    public DbSet<Wytwornia> Wytwornie { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Data Source=localhost;Initial Catalog=localmp;User ID=sa;Password=Password2424%%;Integrated Security=False;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        modelBuilder.Entity<Muzyk>()
            .Property(m => m.IdMuzyk)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Utwor>()
            .Property(u => u.IdUtwor)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Album>()
            .Property(a => a.IdAlbum)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Wytwornia>()
            .Property(w => w.IdWytwornia)
            .ValueGeneratedOnAdd();
        
        
        modelBuilder.Entity<WykonawcaUtworu>()
            .HasKey(w => new { w.IdMuzyk, w.IdUtwor });

        modelBuilder.Entity<WykonawcaUtworu>()
            .HasOne(w => w.Muzycy)
            .WithMany(w => w.Wykonawcy)
            .HasForeignKey(w => w.IdUtwor) .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WykonawcaUtworu>()
            .HasOne(w => w.Utwory)
            .WithMany(w => w.Wykonawcy)
            .HasForeignKey(w => w.IdMuzyk) .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Album>()
            .HasOne(a => a.Wytwornie)
            .WithMany(a => a.Albumy)
            .HasForeignKey(a => a.IdWytwornia) .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Utwor>()
            .HasOne(u => u.Albumy)
            .WithMany(a => a.Utwory)
            .HasForeignKey(a => a.IdAlbum) .OnDelete(DeleteBehavior.Cascade);
            //on delete(DeleteBehavior.Cascade);
    }
}