using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLearner.Migrations
{
    /// <inheritdoc />
    public partial class addeddecksinprojectentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Decks_ProjectId",
                table: "Decks",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Projects_ProjectId",
                table: "Decks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Projects_ProjectId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Decks_ProjectId",
                table: "Decks");
        }
    }
}
