using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addBuilding : Migration
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
                name: "FK_Building_ConstructionCompanies_ConstructionCompanyId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_User_ManagerId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Apartment_ApartmentId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Building_BuildingId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owner",
                table: "Owner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Building",
                table: "Building");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment");

            migrationBuilder.RenameTable(
                name: "Owner",
                newName: "Owners");

            migrationBuilder.RenameTable(
                name: "Building",
                newName: "Buildings");

            migrationBuilder.RenameTable(
                name: "Apartment",
                newName: "Apartments");

            migrationBuilder.RenameIndex(
                name: "IX_Building_ManagerId",
                table: "Buildings",
                newName: "IX_Buildings_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Building_ConstructionCompanyId",
                table: "Buildings",
                newName: "IX_Buildings_ConstructionCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_OwnerId",
                table: "Apartments",
                newName: "IX_Apartments_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_BuildingId",
                table: "Apartments",
                newName: "IX_Apartments_BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Owners_OwnerId",
                table: "Apartments",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ConstructionCompanies_ConstructionCompanyId",
                table: "Buildings",
                column: "ConstructionCompanyId",
                principalTable: "ConstructionCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_User_ManagerId",
                table: "Buildings",
                column: "ManagerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Apartments_ApartmentId",
                table: "Tickets",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Buildings_BuildingId",
                table: "Tickets",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Owners_OwnerId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ConstructionCompanies_ConstructionCompanyId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_User_ManagerId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Apartments_ApartmentId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Buildings_BuildingId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments");

            migrationBuilder.RenameTable(
                name: "Owners",
                newName: "Owner");

            migrationBuilder.RenameTable(
                name: "Buildings",
                newName: "Building");

            migrationBuilder.RenameTable(
                name: "Apartments",
                newName: "Apartment");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_ManagerId",
                table: "Building",
                newName: "IX_Building_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_ConstructionCompanyId",
                table: "Building",
                newName: "IX_Building_ConstructionCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartment",
                newName: "IX_Apartment_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_BuildingId",
                table: "Apartment",
                newName: "IX_Apartment_BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owner",
                table: "Owner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Building",
                table: "Building",
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
                name: "FK_Building_ConstructionCompanies_ConstructionCompanyId",
                table: "Building",
                column: "ConstructionCompanyId",
                principalTable: "ConstructionCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_User_ManagerId",
                table: "Building",
                column: "ManagerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Apartment_ApartmentId",
                table: "Tickets",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Building_BuildingId",
                table: "Tickets",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");
        }
    }
}
