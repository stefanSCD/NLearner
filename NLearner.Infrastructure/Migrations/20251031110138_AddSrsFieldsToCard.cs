using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NLearner.Migrations
{
    /// <inheritdoc />
    public partial class AddSrsFieldsToCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "EaseFactor",
                table: "Cards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "IntervalDays",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextReviewDate",
                table: "Cards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Repetitions",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EaseFactor",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "IntervalDays",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "NextReviewDate",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Repetitions",
                table: "Cards");
        }
    }
}
