using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerTry.Data.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketAttachments_TicketHistories_TicketHistoryId",
                table: "TicketAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketHistories_Tickets_TicketId",
                table: "TicketHistories");

            migrationBuilder.DropIndex(
                name: "IX_TicketHistories_TicketId",
                table: "TicketHistories");

            migrationBuilder.DropIndex(
                name: "IX_TicketAttachments_TicketHistoryId",
                table: "TicketAttachments");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketHistories");

            migrationBuilder.AddColumn<int>(
                name: "TicketHistoryId",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketHistoryId",
                table: "Tickets",
                column: "TicketHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketHistories_TicketHistoryId",
                table: "Tickets",
                column: "TicketHistoryId",
                principalTable: "TicketHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketHistories_TicketHistoryId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketHistoryId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketHistoryId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistories_TicketId",
                table: "TicketHistories",
                column: "TicketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_TicketHistoryId",
                table: "TicketAttachments",
                column: "TicketHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAttachments_TicketHistories_TicketHistoryId",
                table: "TicketAttachments",
                column: "TicketHistoryId",
                principalTable: "TicketHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketHistories_Tickets_TicketId",
                table: "TicketHistories",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
