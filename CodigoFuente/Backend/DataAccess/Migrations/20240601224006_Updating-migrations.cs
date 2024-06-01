using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Updatingmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Buildings_BuildingId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_BuildingId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Invitations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceOperatorId",
                table: "Buildings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_MaintenanceOperatorId",
                table: "Buildings",
                column: "MaintenanceOperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_User_MaintenanceOperatorId",
                table: "Buildings",
                column: "MaintenanceOperatorId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_User_MaintenanceOperatorId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_MaintenanceOperatorId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "MaintenanceOperatorId",
                table: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_BuildingId",
                table: "User",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Buildings_BuildingId",
                table: "User",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }
    }
}
