﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using koloswyklady.Context;

#nullable disable

namespace koloswyklady.Migrations
{
    [DbContext(typeof(Sqlcontext))]
    [Migration("20240609142443_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("koloswyklady.Models.BoatStandard", b =>
                {
                    b.Property<int>("IdBoatStandard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBoatStandard"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdBoatStandard");

                    b.ToTable("BoatStandards");
                });

            modelBuilder.Entity("koloswyklady.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClient"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Emial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdClientCategory")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdClient");

                    b.HasIndex("IdClientCategory");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("koloswyklady.Models.ClientCateogry", b =>
                {
                    b.Property<int>("IdClientCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClientCategory"));

                    b.Property<int>("DiscountPerc")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdClientCategory");

                    b.ToTable("ClientCateogries");
                });

            modelBuilder.Entity("koloswyklady.Models.Reservation", b =>
                {
                    b.Property<int>("IdReservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReservation"));

                    b.Property<string>("CancelReason")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("Fullfilled")
                        .HasColumnType("int");

                    b.Property<int>("IdBoatStandard")
                        .HasColumnType("int");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfBoats")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.HasKey("IdReservation");

                    b.HasIndex("IdBoatStandard");

                    b.HasIndex("IdClient");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("koloswyklady.Models.SailBoat", b =>
                {
                    b.Property<int>("IdSailboat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSailboat"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdBoatStandard")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("IdSailboat");

                    b.HasIndex("IdBoatStandard");

                    b.ToTable("SailBoats");
                });

            modelBuilder.Entity("koloswyklady.Models.Sailboat_Reservation", b =>
                {
                    b.Property<int>("IdSailboat")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("IdReservation")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("IdSailboat", "IdReservation");

                    b.HasIndex("IdReservation");

                    b.ToTable("SailboatReservations");
                });

            modelBuilder.Entity("koloswyklady.Models.Client", b =>
                {
                    b.HasOne("koloswyklady.Models.ClientCateogry", "ClientCateogry")
                        .WithMany("Clients")
                        .HasForeignKey("IdClientCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCateogry");
                });

            modelBuilder.Entity("koloswyklady.Models.Reservation", b =>
                {
                    b.HasOne("koloswyklady.Models.BoatStandard", "BoatStandard")
                        .WithMany("Reservations")
                        .HasForeignKey("IdBoatStandard")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("koloswyklady.Models.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoatStandard");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("koloswyklady.Models.SailBoat", b =>
                {
                    b.HasOne("koloswyklady.Models.BoatStandard", "BoatStandard")
                        .WithMany("SailBoats")
                        .HasForeignKey("IdBoatStandard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoatStandard");
                });

            modelBuilder.Entity("koloswyklady.Models.Sailboat_Reservation", b =>
                {
                    b.HasOne("koloswyklady.Models.Reservation", "Reservation")
                        .WithMany("SailboatReservations")
                        .HasForeignKey("IdReservation")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("koloswyklady.Models.SailBoat", "SailBoat")
                        .WithMany("SailboatReservations")
                        .HasForeignKey("IdSailboat")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("SailBoat");
                });

            modelBuilder.Entity("koloswyklady.Models.BoatStandard", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("SailBoats");
                });

            modelBuilder.Entity("koloswyklady.Models.Client", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("koloswyklady.Models.ClientCateogry", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("koloswyklady.Models.Reservation", b =>
                {
                    b.Navigation("SailboatReservations");
                });

            modelBuilder.Entity("koloswyklady.Models.SailBoat", b =>
                {
                    b.Navigation("SailboatReservations");
                });
#pragma warning restore 612, 618
        }
    }
}
