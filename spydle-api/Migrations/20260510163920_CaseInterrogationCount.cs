using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace spydle_api.Migrations
{
    /// <inheritdoc />
    public partial class CaseInterrogationCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterrogationCount",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterrogationCount",
                table: "Cases");
        }
    }
}
