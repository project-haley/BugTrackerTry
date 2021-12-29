using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerTry.Data.Migrations
{
    public partial class _015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketSnapshot_TicketHistories_TicketHistoryId",
                table: "TicketSnapshot");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSnapshot_Tickets_TicketId",
                table: "TicketSnapshot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketSnapshot",
                table: "TicketSnapshot");

            migrationBuilder.RenameTable(
                name: "TicketSnapshot",
                newName: "TicketSnapshots");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSnapshot_TicketId",
                table: "TicketSnapshots",
                newName: "IX_TicketSnapshots_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSnapshot_TicketHistoryId",
                table: "TicketSnapshots",
                newName: "IX_TicketSnapshots_TicketHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketSnapshots",
                table: "TicketSnapshots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSnapshots_TicketHistories_TicketHistoryId",
                table: "TicketSnapshots",
                column: "TicketHistoryId",
                principalTable: "TicketHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSnapshots_Tickets_TicketId",
                table: "TicketSnapshots",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketSnapshots_TicketHistories_TicketHistoryId",
                table: "TicketSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSnapshots_Tickets_TicketId",
                table: "TicketSnapshots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketSnapshots",
                table: "TicketSnapshots");

            migrationBuilder.RenameTable(
                name: "TicketSnapshots",
                newName: "TicketSnapshot");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSnapshots_TicketId",
                table: "TicketSnapshot",
                newName: "IX_TicketSnapshot_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSnapshots_TicketHistoryId",
                table: "TicketSnapshot",
                newName: "IX_TicketSnapshot_TicketHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketSnapshot",
                table: "TicketSnapshot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSnapshot_TicketHistories_TicketHistoryId",
                table: "TicketSnapshot",
                column: "TicketHistoryId",
                principalTable: "TicketHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSnapshot_Tickets_TicketId",
                table: "TicketSnapshot",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
