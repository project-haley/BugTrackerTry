using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerTry.Data.Migrations
{
    public partial class _016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketSnapshots_Tickets_TicketId",
                table: "TicketSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_TicketSnapshots_TicketId",
                table: "TicketSnapshots");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketSnapshots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketSnapshots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketSnapshots_TicketId",
                table: "TicketSnapshots",
                column: "TicketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSnapshots_Tickets_TicketId",
                table: "TicketSnapshots",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
