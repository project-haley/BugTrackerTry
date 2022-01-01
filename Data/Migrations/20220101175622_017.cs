using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerTry.Data.Migrations
{
    public partial class _017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Resolved",
                table: "TicketSnapshots",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TicketStatus",
                table: "TicketSnapshots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketType",
                table: "TicketSnapshots",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resolved",
                table: "TicketSnapshots");

            migrationBuilder.DropColumn(
                name: "TicketStatus",
                table: "TicketSnapshots");

            migrationBuilder.DropColumn(
                name: "TicketType",
                table: "TicketSnapshots");
        }
    }
}
