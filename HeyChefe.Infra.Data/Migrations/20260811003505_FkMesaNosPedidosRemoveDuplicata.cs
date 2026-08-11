using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FkMesaNosPedidosRemoveDuplicata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos",
                column: "MesaId",
                principalTable: "tb_mesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos");

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
    }
}
