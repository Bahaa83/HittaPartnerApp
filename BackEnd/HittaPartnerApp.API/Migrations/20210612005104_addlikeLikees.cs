using Microsoft.EntityFrameworkCore.Migrations;

namespace HittaPartnerApp.API.Migrations
{
    public partial class addlikeLikees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikerID = table.Column<string>(nullable: false),
                    LikeeID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.LikerID, x.LikeeID });
                    table.ForeignKey(
                        name: "FK_Likes_users_LikeeID",
                        column: x => x.LikeeID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_users_LikerID",
                        column: x => x.LikerID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikeeID",
                table: "Likes",
                column: "LikeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");
        }
    }
}
