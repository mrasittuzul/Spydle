using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace spydle_api.Migrations
{
    /// <inheritdoc />
    public partial class SeedCharacters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Characters",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Code", "IsActive" },
                values: new object[,]
                {
                    { 0, true },
                    { 1, true },
                    { 2, true },
                    { 3, true },
                    { 4, true },
                    { 5, true },
                    { 6, true },
                    { 7, true },
                    { 8, true },
                    { 9, true },
                    { 10, true },
                    { 11, true },
                    { 12, true },
                    { 13, true },
                    { 14, true },
                    { 15, true },
                    { 16, true },
                    { 17, true },
                    { 18, true },
                    { 19, true },
                    { 20, true },
                    { 21, true },
                    { 22, true },
                    { 23, true },
                    { 24, true },
                    { 25, true },
                    { 26, true },
                    { 27, true },
                    { 28, true },
                    { 29, true },
                    { 30, true },
                    { 31, true },
                    { 32, true },
                    { 33, true },
                    { 34, true },
                    { 35, true },
                    { 36, true },
                    { 37, true },
                    { 38, true },
                    { 39, true },
                    { 40, true },
                    { 41, true },
                    { 42, true },
                    { 43, true },
                    { 44, true },
                    { 45, true },
                    { 46, true },
                    { 47, true },
                    { 48, true },
                    { 49, true },
                    { 50, true },
                    { 51, true },
                    { 52, true },
                    { 53, true },
                    { 54, true },
                    { 55, true },
                    { 56, true },
                    { 57, true },
                    { 58, true },
                    { 59, true },
                    { 60, true },
                    { 61, true },
                    { 62, true },
                    { 63, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Code",
                keyValue: 63);

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Characters",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
