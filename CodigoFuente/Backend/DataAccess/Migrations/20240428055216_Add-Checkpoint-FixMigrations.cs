using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckpointFixMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Buildings_buildingId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "buildingId",
                table: "User",
                newName: "BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_User_buildingId",
                table: "User",
                newName: "IX_User_BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Buildings_BuildingId",
                table: "User",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Buildings_BuildingId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "BuildingId",
                table: "User",
                newName: "buildingId");

            migrationBuilder.RenameIndex(
                name: "IX_User_BuildingId",
                table: "User",
                newName: "IX_User_buildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Buildings_buildingId",
                table: "User",
                column: "buildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }
    }
}
