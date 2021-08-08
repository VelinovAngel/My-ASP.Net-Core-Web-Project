using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesBooking.Data.Migrations
{
    public partial class AddReversePropertyForDealerAndClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_AddedClientId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_AspNetUsers_AddedDealerId",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_AddedDealerId",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_AddedClientId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "AddedDealerId",
                table: "Dealers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AddedClientId",
                table: "Clients",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_AspNetUsers_UserId",
                table: "Dealers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_UserId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_AspNetUsers_UserId",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_UserId",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Dealers",
                newName: "AddedDealerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Clients",
                newName: "AddedClientId");

            migrationBuilder.AddColumn<string>(
                name: "DealerId",
                table: "Dealers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_AddedDealerId",
                table: "Dealers",
                column: "AddedDealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AddedClientId",
                table: "Clients",
                column: "AddedClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_AddedClientId",
                table: "Clients",
                column: "AddedClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_AspNetUsers_AddedDealerId",
                table: "Dealers",
                column: "AddedDealerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
