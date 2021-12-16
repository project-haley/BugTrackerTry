using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerTry.Data.Migrations
{
    public partial class _007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_AspNetUsers_ProjectUserId1",
                table: "TicketComments");

            migrationBuilder.DropIndex(
                name: "IX_TicketComments_ProjectUserId1",
                table: "TicketComments");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "TicketComments");

            migrationBuilder.RenameColumn(
                name: "ProjectUserId1",
                table: "TicketComments",
                newName: "CommentBody");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectUserId",
                table: "TicketComments",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_ProjectUserId",
                table: "TicketComments",
                column: "ProjectUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_AspNetUsers_ProjectUserId",
                table: "TicketComments",
                column: "ProjectUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_AspNetUsers_ProjectUserId",
                table: "TicketComments");

            migrationBuilder.DropIndex(
                name: "IX_TicketComments_ProjectUserId",
                table: "TicketComments");

            migrationBuilder.RenameColumn(
                name: "CommentBody",
                table: "TicketComments",
                newName: "ProjectUserId1");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectUserId",
                table: "TicketComments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "TicketComments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_ProjectUserId1",
                table: "TicketComments",
                column: "ProjectUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_AspNetUsers_ProjectUserId1",
                table: "TicketComments",
                column: "ProjectUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
