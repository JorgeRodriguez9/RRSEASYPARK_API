using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRSEASYPARK.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_users_UserId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_reservations_Clients_ClientId",
                table: "reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "ClientParkingLot");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "reservations",
                newName: "ClientParkingLotId");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_ClientId",
                table: "reservations",
                newName: "IX_reservations_ClientParkingLotId");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_UserId",
                table: "ClientParkingLot",
                newName: "IX_ClientParkingLot_UserId");

            migrationBuilder.AddColumn<string>(
                name: "disabilityservices",
                table: "parkingLots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientParkingLot",
                table: "ClientParkingLot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientParkingLot_users_UserId",
                table: "ClientParkingLot",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_ClientParkingLot_ClientParkingLotId",
                table: "reservations",
                column: "ClientParkingLotId",
                principalTable: "ClientParkingLot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientParkingLot_users_UserId",
                table: "ClientParkingLot");

            migrationBuilder.DropForeignKey(
                name: "FK_reservations_ClientParkingLot_ClientParkingLotId",
                table: "reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientParkingLot",
                table: "ClientParkingLot");

            migrationBuilder.DropColumn(
                name: "disabilityservices",
                table: "parkingLots");

            migrationBuilder.RenameTable(
                name: "ClientParkingLot",
                newName: "Clients");

            migrationBuilder.RenameColumn(
                name: "ClientParkingLotId",
                table: "reservations",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_ClientParkingLotId",
                table: "reservations",
                newName: "IX_reservations_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientParkingLot_UserId",
                table: "Clients",
                newName: "IX_Clients_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_users_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_Clients_ClientId",
                table: "reservations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
