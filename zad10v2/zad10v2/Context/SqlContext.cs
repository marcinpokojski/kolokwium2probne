using Microsoft.EntityFrameworkCore;
using zad10v2.Models;

namespace zad10v2.Context;

public class SqlContext : DbContext
{
    protected SqlContext()
    {
    }

    public SqlContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<AppUser> Users { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Data Source=localhost;Initial Catalog=zad11;User ID=sa;Password=Password2424%%;Integrated Security=False;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //
        modelBuilder.Entity<Patient>()
            .Property(c => c.IdPatient)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Doctor>()
            .Property(c => c.IdDoctor)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Prescription>()
            .Property(c => c.IdPrescription)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Medicament>()
            .Property(c => c.IdMedicament)
            .ValueGeneratedOnAdd();

        //
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(k => new { k.IdMedicament, k.IdPrescription });

        //
        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(c => c.Medicament)
            .WithMany(c => c.PrescriptionMedicaments)
            .HasForeignKey(c => c.IdMedicament)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(c => c.Prescription)
            .WithMany(c => c.PrescriptionMedicaments)
            .HasForeignKey(c => c.IdPrescription)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdDoctor).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdPatient).OnDelete(DeleteBehavior.Cascade);
        
    }
}