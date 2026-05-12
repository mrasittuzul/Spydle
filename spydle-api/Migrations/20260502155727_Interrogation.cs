using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace spydle_api.Migrations
{
    /// <inheritdoc />
    public partial class Interrogation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interrogation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Suspects = table.Column<int[]>(type: "integer[]", nullable: false),
                    FoundMatchingTrait = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interrogation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interrogation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interrogation_Cases_CaseDate",
                        column: x => x.CaseDate,
                        principalTable: "Cases",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interrogation_CaseDate",
                table: "Interrogation",
                column: "CaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_Interrogation_UserId",
                table: "Interrogation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interrogation");
        }
    }
}
