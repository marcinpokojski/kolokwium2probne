using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace probnygakko.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utwory_Albumy_IdAlbum",
                table: "Utwory");

            migrationBuilder.AddForeignKey(
                name: "FK_Utwory_Albumy_IdAlbum",
                table: "Utwory",
                column: "IdAlbum",
                principalTable: "Albumy",
                principalColumn: "IdAlbum",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utwory_Albumy_IdAlbum",
                table: "Utwory");

            migrationBuilder.AddForeignKey(
                name: "FK_Utwory_Albumy_IdAlbum",
                table: "Utwory",
                column: "IdAlbum",
                principalTable: "Albumy",
                principalColumn: "IdAlbum");
        }
    }
}
