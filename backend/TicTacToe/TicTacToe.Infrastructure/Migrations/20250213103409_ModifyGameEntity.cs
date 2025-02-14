using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TicTacToe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyGameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.AddColumn<bool>(
                name: "IsRunning",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Moves",
                table: "Games",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldAiMove",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRunning",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Moves",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ShouldAiMove",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameEntityId = table.Column<long>(type: "bigint", nullable: true),
                    Col = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    MoveSymbol = table.Column<char>(type: "character(1)", nullable: false),
                    Row = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Games_GameEntityId",
                        column: x => x.GameEntityId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GameEntityId",
                table: "Moves",
                column: "GameEntityId");
        }
    }
}
