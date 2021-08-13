namespace BikesBooking.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangePropertyReviewOfMotorcycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycles_Reviews_ReviewId",
                table: "Motorcycles");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycles_ReviewId",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Motorcycles");

            migrationBuilder.AlterColumn<int>(
                name: "MotorcycleId",
                table: "Votes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MotorcycleId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Vote",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MotorcycleId",
                table: "Reviews",
                column: "MotorcycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Motorcycles_MotorcycleId",
                table: "Reviews",
                column: "MotorcycleId",
                principalTable: "Motorcycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Motorcycles_MotorcycleId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MotorcycleId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MotorcycleId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Vote",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "MotorcycleId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Motorcycles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_ReviewId",
                table: "Motorcycles",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycles_Reviews_ReviewId",
                table: "Motorcycles",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
