﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Allup.Migrations
{
    public partial class AddColumnIsDeactiveToNewsletterFormsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "NewsletterForms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "NewsletterForms");
        }
    }
}
