using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesBooking.Data.Migrations
{
    public partial class AddNewRelationBetweenClientAndOfferWithOneNewProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Clients_ClientId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ClientId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Offers",
                newName: "StatisticsBooked");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "Offers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_OfferId",
                table: "Clients",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Offers_OfferId",
                table: "Clients",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Offers_OfferId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_OfferId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "StatisticsBooked",
                table: "Offers",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ClientId",
                table: "Offers",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Clients_ClientId",
                table: "Offers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
