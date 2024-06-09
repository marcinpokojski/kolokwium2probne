using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace koloswyklady.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatStandards",
                columns: table => new
                {
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatStandards", x => x.IdBoatStandard);
                });

            migrationBuilder.CreateTable(
                name: "ClientCateogries",
                columns: table => new
                {
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountPerc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCateogries", x => x.IdClientCategory);
                });

            migrationBuilder.CreateTable(
                name: "SailBoats",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SailBoats", x => x.IdSailboat);
                    table.ForeignKey(
                        name: "FK_SailBoats_BoatStandards_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandards",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Emial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_Clients_ClientCateogries_IdClientCategory",
                        column: x => x.IdClientCategory,
                        principalTable: "ClientCateogries",
                        principalColumn: "IdClientCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    NumberOfBoats = table.Column<int>(type: "int", nullable: false),
                    Fullfilled = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_BoatStandards_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandards",
                        principalColumn: "IdBoatStandard");
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SailboatReservations",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false),
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SailboatReservations", x => new { x.IdSailboat, x.IdReservation });
                    table.ForeignKey(
                        name: "FK_SailboatReservations_Reservations_IdReservation",
                        column: x => x.IdReservation,
                        principalTable: "Reservations",
                        principalColumn: "IdReservation");
                    table.ForeignKey(
                        name: "FK_SailboatReservations_SailBoats_IdSailboat",
                        column: x => x.IdSailboat,
                        principalTable: "SailBoats",
                        principalColumn: "IdSailboat");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IdClientCategory",
                table: "Clients",
                column: "IdClientCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdBoatStandard",
                table: "Reservations",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdClient",
                table: "Reservations",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_SailboatReservations_IdReservation",
                table: "SailboatReservations",
                column: "IdReservation");

            migrationBuilder.CreateIndex(
                name: "IX_SailBoats_IdBoatStandard",
                table: "SailBoats",
                column: "IdBoatStandard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SailboatReservations");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "SailBoats");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "BoatStandards");

            migrationBuilder.DropTable(
                name: "ClientCateogries");
        }
    }
}
