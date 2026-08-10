using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CampoDataHoraRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraRegistro",
                table: "tb_usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraRegistro",
                table: "tb_pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraRegistro",
                table: "tb_mesa",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraRegistro",
                table: "tb_linhas_pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraRegistro",
                table: "tb_itens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraRegistro",
                table: "tb_categorias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraRegistro",
                table: "tb_usuarios");

            migrationBuilder.DropColumn(
                name: "DataHoraRegistro",
                table: "tb_pedidos");

            migrationBuilder.DropColumn(
                name: "DataHoraRegistro",
                table: "tb_mesa");

            migrationBuilder.DropColumn(
                name: "DataHoraRegistro",
                table: "tb_linhas_pedidos");

            migrationBuilder.DropColumn(
                name: "DataHoraRegistro",
                table: "tb_itens");

            migrationBuilder.DropColumn(
                name: "DataHoraRegistro",
                table: "tb_categorias");
        }
    }
}
