using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAppExample.Infra.Data.Migrations
{
    public partial class Alteracoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Enderecos_EnderecoId",
                table: "Contatos");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnderecoId",
                table: "Contatos",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Enderecos_EnderecoId",
                table: "Contatos",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Enderecos_EnderecoId",
                table: "Contatos");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnderecoId",
                table: "Contatos",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Enderecos_EnderecoId",
                table: "Contatos",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
