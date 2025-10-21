using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLearner.Migrations
{
    /// <inheritdoc />
    public partial class ChangedprojecTidtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Projects_ProjectId1",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ProjectId1",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ProjectId",
                table: "Notes",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Projects_ProjectId",
                table: "Notes",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Projects_ProjectId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ProjectId",
                table: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
