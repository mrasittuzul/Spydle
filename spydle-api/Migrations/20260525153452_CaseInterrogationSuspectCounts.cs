using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spydle_api.Migrations
{
    /// <inheritdoc />
    public partial class CaseInterrogationSuspectCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinalInterrogationSuspectCount",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegularInterrogationSuspectCount",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalInterrogationSuspectCount",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "RegularInterrogationSuspectCount",
                table: "Cases");
        }
    }
}
