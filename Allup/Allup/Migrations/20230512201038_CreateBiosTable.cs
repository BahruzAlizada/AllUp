using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Allup.Migrations
{
    public partial class CreateBiosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterOpenDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FooterCloseDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FooterOpenTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FooterCloseTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bios");
        }
    }
}
