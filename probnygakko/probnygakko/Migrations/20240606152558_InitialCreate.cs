using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace probnygakko.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Muzycy",
                columns: table => new
                {
                    IdMuzyk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Pseudonim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muzycy", x => x.IdMuzyk);
                });

            migrationBuilder.CreateTable(
                name: "Wytwornie",
                columns: table => new
                {
                    IdWytwornia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wytwornie", x => x.IdWytwornia);
                });

            migrationBuilder.CreateTable(
                name: "Albumy",
                columns: table => new
                {
                    IdAlbum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaAlbumu = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    DataWydania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdWytwornia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumy", x => x.IdAlbum);
                    table.ForeignKey(
                        name: "FK_Albumy_Wytwornie_IdWytwornia",
                        column: x => x.IdWytwornia,
                        principalTable: "Wytwornie",
                        principalColumn: "IdWytwornia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utwory",
                columns: table => new
                {
                    IdUtwor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaUtworu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CzasTrwania = table.Column<float>(type: "real", nullable: false),
                    IdAlbum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utwory", x => x.IdUtwor);
                    table.ForeignKey(
                        name: "FK_Utwory_Albumy_IdAlbum",
                        column: x => x.IdAlbum,
                        principalTable: "Albumy",
                        principalColumn: "IdAlbum");
                });

            migrationBuilder.CreateTable(
                name: "Wykonawcy",
                columns: table => new
                {
                    IdMuzyk = table.Column<int>(type: "int", nullable: false),
                    IdUtwor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wykonawcy", x => new { x.IdMuzyk, x.IdUtwor });
                    table.ForeignKey(
                        name: "FK_Wykonawcy_Muzycy_IdUtwor",
                        column: x => x.IdUtwor,
                        principalTable: "Muzycy",
                        principalColumn: "IdMuzyk",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wykonawcy_Utwory_IdMuzyk",
                        column: x => x.IdMuzyk,
                        principalTable: "Utwory",
                        principalColumn: "IdUtwor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albumy_IdWytwornia",
                table: "Albumy",
                column: "IdWytwornia");

            migrationBuilder.CreateIndex(
                name: "IX_Utwory_IdAlbum",
                table: "Utwory",
                column: "IdAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_Wykonawcy_IdUtwor",
                table: "Wykonawcy",
                column: "IdUtwor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wykonawcy");

            migrationBuilder.DropTable(
                name: "Muzycy");

            migrationBuilder.DropTable(
                name: "Utwory");

            migrationBuilder.DropTable(
                name: "Albumy");

            migrationBuilder.DropTable(
                name: "Wytwornie");
        }
    }
}
