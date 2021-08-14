using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesBooking.Data.Migrations
{
    public partial class IntroduceImgUrlInDealerAndIsApprovedInMotrocycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Motorcycles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Dealers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Dealers");
        }
    }
}
