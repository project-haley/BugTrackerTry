using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerTry.Data.Migrations
{
    public partial class _008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_TicketHistories_TicketHistoryId",
                table: "TicketComments");

            migrationBuilder.AlterColumn<int>(
                name: "TicketHistoryId",
                table: "TicketComments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_TicketHistories_TicketHistoryId",
                table: "TicketComments",
                column: "TicketHistoryId",
                principalTable: "TicketHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_TicketHistories_TicketHistoryId",
                table: "TicketComments");

            migrationBuilder.AlterColumn<int>(
                name: "TicketHistoryId",
                table: "TicketComments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_TicketHistories_TicketHistoryId",
                table: "TicketComments",
                column: "TicketHistoryId",
                principalTable: "TicketHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
