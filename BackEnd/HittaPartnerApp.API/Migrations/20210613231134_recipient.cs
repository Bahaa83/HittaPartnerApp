using Microsoft.EntityFrameworkCore.Migrations;

namespace HittaPartnerApp.API.Migrations
{
    public partial class recipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_users_RecipienID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipienID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecipienID",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "RecipientID",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientID",
                table: "Messages",
                column: "RecipientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_users_RecipientID",
                table: "Messages",
                column: "RecipientID",
                principalTable: "users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_users_RecipientID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipientID",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "RecipientID",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipienID",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipienID",
                table: "Messages",
                column: "RecipienID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_users_RecipienID",
                table: "Messages",
                column: "RecipienID",
                principalTable: "users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
