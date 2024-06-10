using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using wa10.Models;

namespace wa10;

public class SqlContext : DbContext
{
    public SqlContext()
    {
    }

    public SqlContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Organiser> Organisers { get; set; }
    public DbSet<EventOrganiser> EventOrganisers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
//
        modelBuilder.Entity<Event>()
            .Property(x => x.IdEvent)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Organiser>()
            .Property(x => x.IdOrganiser)
            .ValueGeneratedOnAdd();
//
        modelBuilder.Entity<EventOrganiser>()
            .HasKey(k => new { k.IdEvent, k.IdOrganiser });
        
        //
        modelBuilder.Entity<EventOrganiser>()
            .HasOne(x => x.Event)
            .WithMany(x => x.EventOrganisers)
            .HasForeignKey(x => x.IdEvent);


        modelBuilder.Entity<EventOrganiser>().HasOne(x => x.Organiser)
            .WithMany(x => x.EventOrganisers)
            .HasForeignKey(x => x.IdOrganiser);
        //

        modelBuilder.Entity<Event>().HasData(new List<Event>()
        {
            new Event()
            {
                IdEvent = 1,
                Name = "Trucja",
                DateFrom = new DateTime(2024, 05, 30)

            },
            new Event()
            {
                IdEvent = 2,
                Name = "Francja",
                DateFrom = new DateTime(2024, 06, 13),
                DateTo = new DateTime(2024, 06, 19)
            }
        });

        modelBuilder.Entity<Organiser>().HasData(new List<Organiser>()
        {
            new Organiser()
            {
                IdOrganiser = 1,
                Name = "Itaka"
            },
            new Organiser()
            {
                IdOrganiser = 2,
                Name = "tui"
            }
        });

        modelBuilder.Entity<EventOrganiser>().HasData(new List<EventOrganiser>()
        {
            new EventOrganiser()
            {
                IdOrganiser = 1,
                IdEvent = 1,
                MainOrganiser = true
            }
        });



    }
}