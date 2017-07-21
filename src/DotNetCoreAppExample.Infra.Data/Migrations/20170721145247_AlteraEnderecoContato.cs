using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAppExample.Infra.Data.Migrations
{
    public partial class AlteraEnderecoContato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Contatos_ContatoId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_ContatoId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "ContatoId",
                table: "Enderecos");

            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoId",
                table: "Contatos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_EnderecoId",
                table: "Contatos",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Enderecos_EnderecoId",
                table: "Contatos",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Enderecos_EnderecoId",
                table: "Contatos");

            migrationBuilder.DropIndex(
                name: "IX_Contatos_EnderecoId",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Contatos");

            migrationBuilder.AddColumn<Guid>(
                name: "ContatoId",
                table: "Enderecos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ContatoId",
                table: "Enderecos",
                column: "ContatoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Contatos_ContatoId",
                table: "Enderecos",
                column: "ContatoId",
                principalTable: "Contatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
