using Microsoft.EntityFrameworkCore.Migrations;

namespace Furs2Feathers.DataAccess.Migrations
{
    public partial class asdsasa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "pet",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "pet",
                type: "character varying",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "pet");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "pet");
        }
    }
}
