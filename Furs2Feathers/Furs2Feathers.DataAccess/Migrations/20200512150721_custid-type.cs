using Microsoft.EntityFrameworkCore.Migrations;

namespace Furs2Feathers.DataAccess.Migrations
{
    public partial class custidtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "customerId",
                table: "pet",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "customerId",
                table: "pet",
                type: "text",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
