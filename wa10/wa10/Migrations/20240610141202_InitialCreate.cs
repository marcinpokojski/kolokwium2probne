using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wa10.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.IdEvent);
                });

            migrationBuilder.CreateTable(
                name: "Organisers",
                columns: table => new
                {
                    IdOrganiser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisers", x => x.IdOrganiser);
                });

            migrationBuilder.CreateTable(
                name: "EventOrganisers",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false),
                    IdOrganiser = table.Column<int>(type: "int", nullable: false),
                    MainOrganiser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOrganisers", x => new { x.IdEvent, x.IdOrganiser });
                    table.ForeignKey(
                        name: "FK_EventOrganisers_Events_IdEvent",
                        column: x => x.IdEvent,
                        principalTable: "Events",
                        principalColumn: "IdEvent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventOrganisers_Organisers_IdOrganiser",
                        column: x => x.IdOrganiser,
                        principalTable: "Organisers",
                        principalColumn: "IdOrganiser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "IdEvent", "DateFrom", "DateTo", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Trucja" },
                    { 2, new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Francja" }
                });

            migrationBuilder.InsertData(
                table: "Organisers",
                columns: new[] { "IdOrganiser", "Name" },
                values: new object[,]
                {
                    { 1, "Itaka" },
                    { 2, "tui" }
                });

            migrationBuilder.InsertData(
                table: "EventOrganisers",
                columns: new[] { "IdEvent", "IdOrganiser", "MainOrganiser" },
                values: new object[] { 1, 1, true });

            migrationBuilder.CreateIndex(
                name: "IX_EventOrganisers_IdOrganiser",
                table: "EventOrganisers",
                column: "IdOrganiser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventOrganisers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Organisers");
        }
    }
}
