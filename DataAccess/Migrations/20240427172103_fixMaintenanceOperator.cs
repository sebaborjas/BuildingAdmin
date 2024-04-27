using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixMaintenanceOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Building_BuildingId",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Owner_OwnerId",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Apartment_ApartmentId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owner",
                table: "Owner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment");

            migrationBuilder.RenameTable(
                name: "Owner",
                newName: "Owners");

            migrationBuilder.RenameTable(
                name: "Apartment",
                newName: "Apartments");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_OwnerId",
                table: "Apartments",
                newName: "IX_Apartments_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_BuildingId",
                table: "Apartments",
                newName: "IX_Apartments_BuildingId");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<int>(
                name: "buildingId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_buildingId",
                table: "User",
                column: "buildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Building_BuildingId",
                table: "Apartments",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Owners_OwnerId",
                table: "Apartments",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Apartments_ApartmentId",
                table: "Tickets",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Building_buildingId",
                table: "User",
                column: "buildingId",
                principalTable: "Building",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Building_BuildingId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Owners_OwnerId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Apartments_ApartmentId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Building_buildingId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_buildingId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "buildingId",
                table: "User");

            migrationBuilder.RenameTable(
                name: "Owners",
                newName: "Owner");

            migrationBuilder.RenameTable(
                name: "Apartments",
                newName: "Apartment");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartment",
                newName: "IX_Apartment_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_BuildingId",
                table: "Apartment",
                newName: "IX_Apartment_BuildingId");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owner",
                table: "Owner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Building_BuildingId",
                table: "Apartment",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Owner_OwnerId",
                table: "Apartment",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Apartment_ApartmentId",
                table: "Tickets",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id");
        }
    }
}
