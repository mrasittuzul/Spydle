using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace spydle_api.Migrations
{
    /// <inheritdoc />
    public partial class EyeAndSkinColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharacterCode",
                table: "Cases",
                newName: "SpyCharacterCode");

            migrationBuilder.AddColumn<int[]>(
                name: "IncludedCharacters",
                table: "Cases",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.CreateTable(
                name: "CharacterEyeColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Red = table.Column<byte>(type: "smallint", nullable: false),
                    Green = table.Column<byte>(type: "smallint", nullable: false),
                    Blue = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEyeColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkinColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Red = table.Column<byte>(type: "smallint", nullable: false),
                    Green = table.Column<byte>(type: "smallint", nullable: false),
                    Blue = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkinColors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CharacterEyeColors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[,]
                {
                    { 0, (byte)30, (byte)58, "Brown", (byte)97 },
                    { 1, (byte)209, (byte)87, "Blue", (byte)46 },
                    { 2, (byte)59, (byte)212, "Green", (byte)125 },
                    { 3, (byte)199, (byte)166, "Grey", (byte)155 }
                });

            migrationBuilder.InsertData(
                table: "CharacterSkinColors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[,]
                {
                    { 0, (byte)214, (byte)214, "Cyan", (byte)41 },
                    { 1, (byte)15, (byte)217, "Yellow", (byte)255 },
                    { 2, (byte)73, (byte)41, "Red", (byte)214 },
                    { 3, (byte)41, (byte)214, "Green", (byte)52 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEyeColors");

            migrationBuilder.DropTable(
                name: "CharacterSkinColors");

            migrationBuilder.DropColumn(
                name: "IncludedCharacters",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "SpyCharacterCode",
                table: "Cases",
                newName: "CharacterCode");
        }
    }
}
