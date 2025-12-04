using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLearner.Migrations
{
    /// <inheritdoc />
    public partial class AddedProjectEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ProjectId1",
                table: "Notes",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Projects_ProjectId1",
                table: "Notes",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Projects_ProjectId1",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ProjectId1",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Notes");
        }
    }
}
