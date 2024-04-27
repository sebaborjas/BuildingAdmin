using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class addCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Administrators_AdministratorId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_AdministratorId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Invitations");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "AdministratorId",
                table: "Invitations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AdministratorId",
                table: "Invitations",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Administrators_AdministratorId",
                table: "Invitations",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id");
        }
    }
}
