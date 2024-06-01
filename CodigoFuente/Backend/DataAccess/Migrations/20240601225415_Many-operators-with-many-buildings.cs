using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Manyoperatorswithmanybuildings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_User_MaintenanceOperatorId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_MaintenanceOperatorId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "MaintenanceOperatorId",
                table: "Buildings");

            migrationBuilder.CreateTable(
                name: "BuildingMaintenanceOperator",
                columns: table => new
                {
                    BuildingsId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceOperatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaintenanceOperator", x => new { x.BuildingsId, x.MaintenanceOperatorId });
                    table.ForeignKey(
                        name: "FK_BuildingMaintenanceOperator_Buildings_BuildingsId",
                        column: x => x.BuildingsId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingMaintenanceOperator_User_MaintenanceOperatorId",
                        column: x => x.MaintenanceOperatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaintenanceOperator_MaintenanceOperatorId",
                table: "BuildingMaintenanceOperator",
                column: "MaintenanceOperatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingMaintenanceOperator");

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
    }
}
