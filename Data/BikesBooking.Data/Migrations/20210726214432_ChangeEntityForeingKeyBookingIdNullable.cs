using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesBooking.Data.Migrations
{
    public partial class ChangeEntityForeingKeyBookingIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offers_BookingId",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Offers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_BookingId",
                table: "Offers",
                column: "BookingId",
                unique: true,
                filter: "[BookingId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offers_BookingId",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_BookingId",
                table: "Offers",
                column: "BookingId",
                unique: true);
        }
    }
}
