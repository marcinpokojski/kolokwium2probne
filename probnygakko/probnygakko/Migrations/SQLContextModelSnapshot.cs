﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using probnygakko.Context;

#nullable disable

namespace probnygakko.Migrations
{
    [DbContext(typeof(SQLContext))]
    partial class SQLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("probnygakko.Models.Album", b =>
                {
                    b.Property<int>("IdAlbum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAlbum"));

                    b.Property<DateTime>("DataWydania")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdWytwornia")
                        .HasColumnType("int");

                    b.Property<string>("NazwaAlbumu")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdAlbum");

                    b.HasIndex("IdWytwornia");

                    b.ToTable("Albumy");
                });

            modelBuilder.Entity("probnygakko.Models.Muzyk", b =>
                {
                    b.Property<int>("IdMuzyk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMuzyk"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Pseudonim")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdMuzyk");

                    b.ToTable("Muzycy");
                });

            modelBuilder.Entity("probnygakko.Models.Utwor", b =>
                {
                    b.Property<int>("IdUtwor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtwor"));

                    b.Property<float>("CzasTrwania")
                        .HasColumnType("real");

                    b.Property<int?>("IdAlbum")
                        .HasColumnType("int");

                    b.Property<string>("NazwaUtworu")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdUtwor");

                    b.HasIndex("IdAlbum");

                    b.ToTable("Utwory");
                });

            modelBuilder.Entity("probnygakko.Models.WykonawcaUtworu", b =>
                {
                    b.Property<int>("IdMuzyk")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("IdUtwor")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("IdMuzyk", "IdUtwor");

                    b.HasIndex("IdUtwor");

                    b.ToTable("Wykonawcy");
                });

            modelBuilder.Entity("probnygakko.Models.Wytwornia", b =>
                {
                    b.Property<int>("IdWytwornia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdWytwornia"));

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdWytwornia");

                    b.ToTable("Wytwornie");
                });

            modelBuilder.Entity("probnygakko.Models.Album", b =>
                {
                    b.HasOne("probnygakko.Models.Wytwornia", "Wytwornie")
                        .WithMany("Albumy")
                        .HasForeignKey("IdWytwornia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wytwornie");
                });

            modelBuilder.Entity("probnygakko.Models.Utwor", b =>
                {
                    b.HasOne("probnygakko.Models.Album", "Albumy")
                        .WithMany("Utwory")
                        .HasForeignKey("IdAlbum")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Albumy");
                });

            modelBuilder.Entity("probnygakko.Models.WykonawcaUtworu", b =>
                {
                    b.HasOne("probnygakko.Models.Utwor", "Utwory")
                        .WithMany("Wykonawcy")
                        .HasForeignKey("IdMuzyk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("probnygakko.Models.Muzyk", "Muzycy")
                        .WithMany("Wykonawcy")
                        .HasForeignKey("IdUtwor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Muzycy");

                    b.Navigation("Utwory");
                });

            modelBuilder.Entity("probnygakko.Models.Album", b =>
                {
                    b.Navigation("Utwory");
                });

            modelBuilder.Entity("probnygakko.Models.Muzyk", b =>
                {
                    b.Navigation("Wykonawcy");
                });

            modelBuilder.Entity("probnygakko.Models.Utwor", b =>
                {
                    b.Navigation("Wykonawcy");
                });

            modelBuilder.Entity("probnygakko.Models.Wytwornia", b =>
                {
                    b.Navigation("Albumy");
                });
#pragma warning restore 612, 618
        }
    }
}
