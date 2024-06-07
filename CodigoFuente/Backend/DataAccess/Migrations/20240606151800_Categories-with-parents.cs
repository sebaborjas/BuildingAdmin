using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Categorieswithparents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelatedToId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RelatedToId",
                table: "Categories",
                column: "RelatedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_RelatedToId",
                table: "Categories",
                column: "RelatedToId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_RelatedToId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RelatedToId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RelatedToId",
                table: "Categories");
        }
    }
}
