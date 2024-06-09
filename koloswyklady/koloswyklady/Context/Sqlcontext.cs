using koloswyklady.Models;
using Microsoft.EntityFrameworkCore;

namespace koloswyklady.Context;

public class Sqlcontext : DbContext
{
    public Sqlcontext()
    {
    }

    public Sqlcontext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ClientCateogry> ClientCateogries { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Sailboat_Reservation> SailboatReservations { get; set; }
    public DbSet<SailBoat> SailBoats { get; set; }
    public DbSet<BoatStandard> BoatStandards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Data Source=localhost;Initial Catalog=gakoprobny;User ID=sa;Password=Password2424%%;Integrated Security=False;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //autogeneracja
        modelBuilder.Entity<ClientCateogry>()
            .Property(c => c.IdClientCategory)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Client>()
            .Property(c => c.IdClient)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Reservation>()
            .Property(c => c.IdReservation)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<BoatStandard>()
            .Property(c => c.IdBoatStandard)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<SailBoat>()
            .Property(c => c.IdSailboat)
            .ValueGeneratedOnAdd();
        
    //klucze glowne
        modelBuilder.Entity<Sailboat_Reservation>()
            .HasKey(s => new { s.IdSailboat, s.IdReservation });
        
    //klucze obce

    modelBuilder.Entity<Client>()
        .HasOne(c => c.ClientCateogry)
        .WithMany(c => c.Clients)
        .HasForeignKey(c => c.IdClientCategory)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<SailBoat>()
        .HasOne(s => s.BoatStandard)
        .WithMany(s => s.SailBoats)
        .HasForeignKey(s => s.IdBoatStandard)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Reservation>()
        .HasOne(r => r.Client)
        .WithMany(r => r.Reservations)
        .HasForeignKey(r => r.IdClient)
        .OnDelete(DeleteBehavior.Cascade);
    modelBuilder.Entity<Reservation>()
        .HasOne(r => r.BoatStandard)
        .WithMany(r => r.Reservations)
        .HasForeignKey(r => r.IdBoatStandard)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Sailboat_Reservation>()
        .HasOne(s => s.Reservation)
        .WithMany(s => s.SailboatReservations)
        .HasForeignKey(s => s.IdReservation)
        .OnDelete(DeleteBehavior.NoAction);
    
    modelBuilder.Entity<Sailboat_Reservation>()
        .HasOne(s => s.SailBoat)
        .WithMany(s => s.SailboatReservations)
        .HasForeignKey(s => s.IdSailboat)
        .OnDelete(DeleteBehavior.NoAction);
    




    }
}