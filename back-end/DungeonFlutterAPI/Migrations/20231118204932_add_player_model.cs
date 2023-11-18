using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonFlutterAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_player_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "HighScores");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "HighScores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HighScores_PlayerId",
                table: "HighScores",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighScores_Players_PlayerId",
                table: "HighScores",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighScores_Players_PlayerId",
                table: "HighScores");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropIndex(
                name: "IX_HighScores_PlayerId",
                table: "HighScores");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "HighScores");

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "HighScores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
