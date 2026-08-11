using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FkMesaNosPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos");

            migrationBuilder.AlterColumn<Guid>(
                name: "MesaId",
                table: "tb_pedidos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MesaId1",
                table: "tb_pedidos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedidos_MesaId1",
                table: "tb_pedidos",
                column: "MesaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos",
                column: "MesaId",
                principalTable: "tb_mesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId1",
                table: "tb_pedidos",
                column: "MesaId1",
                principalTable: "tb_mesas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId1",
                table: "tb_pedidos");

            migrationBuilder.DropIndex(
                name: "IX_tb_pedidos_MesaId1",
                table: "tb_pedidos");

            migrationBuilder.DropColumn(
                name: "MesaId1",
                table: "tb_pedidos");

            migrationBuilder.AlterColumn<Guid>(
                name: "MesaId",
                table: "tb_pedidos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos",
                column: "MesaId",
                principalTable: "tb_mesas",
                principalColumn: "Id");
        }
    }
}
