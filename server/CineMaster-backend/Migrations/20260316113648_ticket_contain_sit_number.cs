using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineMaster_backend.Migrations
{
    /// <inheritdoc />
    public partial class ticket_contain_sit_number : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SitNumber",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SitNumber",
                table: "Ticket");
        }
    }
}
