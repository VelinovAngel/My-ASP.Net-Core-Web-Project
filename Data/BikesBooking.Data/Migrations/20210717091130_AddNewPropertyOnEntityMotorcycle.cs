using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesBooking.Data.Migrations
{
    public partial class AddNewPropertyOnEntityMotorcycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Motorcycles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Motorcycles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Motorcycles");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Motorcycles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
