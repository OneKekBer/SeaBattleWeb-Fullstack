using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeaBattleWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameGameState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Games",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Games",
                newName: "State");
        }
    }
}
