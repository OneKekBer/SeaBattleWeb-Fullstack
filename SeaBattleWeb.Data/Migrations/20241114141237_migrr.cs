using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeaBattleWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SecondPlayerConnectionId",
                table: "Games",
                type: "character varying(90)",
                maxLength: 90,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstPlayerConnectionId",
                table: "Games",
                type: "character varying(90)",
                maxLength: 90,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SecondPlayerConnectionId",
                table: "Games",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(90)",
                oldMaxLength: 90);

            migrationBuilder.AlterColumn<string>(
                name: "FirstPlayerConnectionId",
                table: "Games",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(90)",
                oldMaxLength: 90);
        }
    }
}
