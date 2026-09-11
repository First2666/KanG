using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanG.Migrations
{
    /// <inheritdoc />
    public partial class AddTripTimeAndCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TripPlanItems",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedCost",
                table: "TripPlanItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TripPlanItems",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TripPlanItems");

            migrationBuilder.DropColumn(
                name: "EstimatedCost",
                table: "TripPlanItems");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TripPlanItems");
        }
    }
}
