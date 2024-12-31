using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWPF.Migrations
{
    /// <inheritdoc />
    public partial class ToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Universities_UniversityId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_UniversityId",
                table: "Groups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Groups_UniversityId",
                table: "Groups",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Universities_UniversityId",
                table: "Groups",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
