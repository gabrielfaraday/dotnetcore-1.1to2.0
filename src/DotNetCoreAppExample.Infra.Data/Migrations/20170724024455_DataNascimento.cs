using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAppExample.Infra.Data.Migrations
{
    public partial class DataNascimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Contatos",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Contatos");
        }
    }
}
