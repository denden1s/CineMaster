using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineMaster_backend.Migrations
{
    /// <inheritdoc />
    public partial class film_with_poster_link : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Film",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Film");
        }
    }
}
