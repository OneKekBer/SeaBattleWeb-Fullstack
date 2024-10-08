using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeaBattleWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class BoardSerialized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardSerialized",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardSerialized",
                table: "Boards");
        }
    }
}
