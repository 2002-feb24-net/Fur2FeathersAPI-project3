using Microsoft.EntityFrameworkCore.Migrations;

namespace Furs2Feathers.DataAccess.Migrations
{
    public partial class namecustidcols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "customerId",
                table: "pet",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "customer",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customerId",
                table: "pet");

            migrationBuilder.DropColumn(
                name: "name",
                table: "customer");
        }
    }
}
