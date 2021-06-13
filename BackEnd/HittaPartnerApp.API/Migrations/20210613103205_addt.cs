using Microsoft.EntityFrameworkCore.Migrations;

namespace HittaPartnerApp.API.Migrations
{
    public partial class addt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientID",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientID",
                table: "Messages");
        }
    }
}
