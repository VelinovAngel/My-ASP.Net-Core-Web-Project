namespace BikesBooking.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeEntityRelationsWithBookingAndOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Bookings_BookingId",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_BookingId",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Motorcycles");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_BookingId",
                table: "Offers",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Bookings_BookingId",
                table: "Offers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Bookings_BookingId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_BookingId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Motorcycles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_BookingId",
                table: "Motorcycles",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Bookings_BookingId",
                table: "Motorcycles",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
