using Microsoft.EntityFrameworkCore.Migrations;

namespace ts_space_shooter_backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HighScoreEntries",
                columns: table => new
                {
                    HighScoreEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerInitials = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighScoreEntries", x => x.HighScoreEntryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighScoreEntries");
        }
    }
}
