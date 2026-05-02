using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spydle_api.Migrations
{
    /// <inheritdoc />
    public partial class InterrogationToInterrogations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interrogation_AspNetUsers_UserId",
                table: "Interrogation");

            migrationBuilder.DropForeignKey(
                name: "FK_Interrogation_Cases_CaseDate",
                table: "Interrogation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interrogation",
                table: "Interrogation");

            migrationBuilder.RenameTable(
                name: "Interrogation",
                newName: "Interrogations");

            migrationBuilder.RenameIndex(
                name: "IX_Interrogation_UserId",
                table: "Interrogations",
                newName: "IX_Interrogations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Interrogation_CaseDate",
                table: "Interrogations",
                newName: "IX_Interrogations_CaseDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interrogations",
                table: "Interrogations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interrogations_AspNetUsers_UserId",
                table: "Interrogations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interrogations_Cases_CaseDate",
                table: "Interrogations",
                column: "CaseDate",
                principalTable: "Cases",
                principalColumn: "Date",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interrogations_AspNetUsers_UserId",
                table: "Interrogations");

            migrationBuilder.DropForeignKey(
                name: "FK_Interrogations_Cases_CaseDate",
                table: "Interrogations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interrogations",
                table: "Interrogations");

            migrationBuilder.RenameTable(
                name: "Interrogations",
                newName: "Interrogation");

            migrationBuilder.RenameIndex(
                name: "IX_Interrogations_UserId",
                table: "Interrogation",
                newName: "IX_Interrogation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Interrogations_CaseDate",
                table: "Interrogation",
                newName: "IX_Interrogation_CaseDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interrogation",
                table: "Interrogation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interrogation_AspNetUsers_UserId",
                table: "Interrogation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interrogation_Cases_CaseDate",
                table: "Interrogation",
                column: "CaseDate",
                principalTable: "Cases",
                principalColumn: "Date",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
