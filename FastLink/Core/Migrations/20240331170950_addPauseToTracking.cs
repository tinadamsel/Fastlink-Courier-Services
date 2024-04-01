using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class addPauseToTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArrivalDate",
                table: "Trackings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepatureDate",
                table: "Trackings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaused",
                table: "Trackings",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Trackings");

            migrationBuilder.DropColumn(
                name: "DepatureDate",
                table: "Trackings");

            migrationBuilder.DropColumn(
                name: "IsPaused",
                table: "Trackings");
        }
    }
}
