using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NpuBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class MultipleElementScoreSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NpuCreations_Users_UserId",
                table: "NpuCreations");

            migrationBuilder.DropIndex(
                name: "IX_NpuCreations_UserId",
                table: "NpuCreations");

            migrationBuilder.DropColumn(
                name: "ElementUsed",
                table: "NpuCreations");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "NpuCreations");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "NpuCreations");

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    ElementId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.ElementId);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    NpuCreationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => new { x.UserId, x.NpuCreationId });
                    table.ForeignKey(
                        name: "FK_Scores_NpuCreations_NpuCreationId",
                        column: x => x.NpuCreationId,
                        principalTable: "NpuCreations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NpuCreationElements",
                columns: table => new
                {
                    NpuCreationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ElementId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NpuCreationElements", x => new { x.NpuCreationId, x.ElementId });
                    table.ForeignKey(
                        name: "FK_NpuCreationElements_Elements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "Elements",
                        principalColumn: "ElementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NpuCreationElements_NpuCreations_NpuCreationId",
                        column: x => x.NpuCreationId,
                        principalTable: "NpuCreations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NpuCreationElements_ElementId",
                table: "NpuCreationElements",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_NpuCreationId",
                table: "Scores",
                column: "NpuCreationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NpuCreationElements");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.AddColumn<string>(
                name: "ElementUsed",
                table: "NpuCreations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "NpuCreations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<string>>(
                name: "Tags",
                table: "NpuCreations",
                type: "text[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_NpuCreations_UserId",
                table: "NpuCreations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NpuCreations_Users_UserId",
                table: "NpuCreations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
