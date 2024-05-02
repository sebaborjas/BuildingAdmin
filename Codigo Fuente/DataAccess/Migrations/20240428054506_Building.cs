using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Building : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Building_BuildingId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_ConstructionCompanies_ConstructionCompanyId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_User_ManagerId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Building_BuildingId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Building_buildingId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_buildingId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Building",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "buildingId",
                table: "User");

            migrationBuilder.RenameTable(
                name: "Building",
                newName: "Buildings");

            migrationBuilder.RenameIndex(
                name: "IX_Building_ManagerId",
                table: "Buildings",
                newName: "IX_Buildings_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Building_ConstructionCompanyId",
                table: "Buildings",
                newName: "IX_Buildings_ConstructionCompanyId");

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
                name: "PK_Buildings",
                table: "Buildings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments",
                column: "BuildingId",
                principalTable: "Buildings",
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
                name: "FK_Buildings_ConstructionCompanies_ConstructionCompanyId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_User_ManagerId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Buildings_BuildingId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings");

            migrationBuilder.RenameTable(
                name: "Buildings",
                newName: "Building");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_ManagerId",
                table: "Building",
                newName: "IX_Building_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_ConstructionCompanyId",
                table: "Building",
                newName: "IX_Building_ConstructionCompanyId");

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
                name: "PK_Building",
                table: "Building",
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
                name: "FK_Tickets_Building_BuildingId",
                table: "Tickets",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Building_buildingId",
                table: "User",
                column: "buildingId",
                principalTable: "Building",
                principalColumn: "Id");
        }
    }
}
