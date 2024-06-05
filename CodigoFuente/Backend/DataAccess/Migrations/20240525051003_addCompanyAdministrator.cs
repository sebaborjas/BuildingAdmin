using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyAdministrator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConstructionCompanyId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ConstructionCompanyId",
                table: "User",
                column: "ConstructionCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ConstructionCompanies_ConstructionCompanyId",
                table: "User",
                column: "ConstructionCompanyId",
                principalTable: "ConstructionCompanies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ConstructionCompanies_ConstructionCompanyId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ConstructionCompanyId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ConstructionCompanyId",
                table: "User");
        }
    }
}
