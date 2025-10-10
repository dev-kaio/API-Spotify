using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Spotify.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuracaoFromMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Musicas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duracao",
                table: "Musicas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
