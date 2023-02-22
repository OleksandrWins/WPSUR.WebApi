using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPSUR.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Emergency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmergencyContent",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyList",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencyContent",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmergencyList",
                table: "User");
        }
    }
}
