using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Spotify.Migrations
{
    /// <inheritdoc />
    public partial class AddAudioECapaBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioBase64",
                table: "Musicas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CapaBase64",
                table: "Musicas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioBase64",
                table: "Musicas");

            migrationBuilder.DropColumn(
                name: "CapaBase64",
                table: "Musicas");
        }
    }
}
