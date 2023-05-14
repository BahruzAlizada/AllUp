using Microsoft.EntityFrameworkCore.Migrations;

namespace Allup.Migrations
{
    public partial class AddIsDeactiveColumnToBrands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "İsDeactive",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "İsDeactive",
                table: "Brands");
        }
    }
}
