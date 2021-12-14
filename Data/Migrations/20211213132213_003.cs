using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerTry.Data.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketAttachments_TicketComments_TicketCommentId",
                table: "TicketAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_TicketHistories_TicketHistoryId",
                table: "TicketComments");

            migrationBuilder.DropIndex(
                name: "IX_TicketAttachments_TicketCommentId",
                table: "TicketAttachments");

            migrationBuilder.DropColumn(
                name: "TicketCommentId",
                table: "TicketAttachments");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TicketCommentId",
                table: "TicketAttachments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_TicketCommentId",
                table: "TicketAttachments",
                column: "TicketCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAttachments_TicketComments_TicketCommentId",
                table: "TicketAttachments",
                column: "TicketCommentId",
                principalTable: "TicketComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_TicketHistories_TicketHistoryId",
                table: "TicketComments",
                column: "TicketHistoryId",
                principalTable: "TicketHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
