using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FkPedidosNaLinhaPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedidos_tb_mesa_MesaId",
                table: "tb_pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_mesa",
                table: "tb_mesa");

            migrationBuilder.RenameTable(
                name: "tb_mesa",
                newName: "tb_mesas");

            migrationBuilder.RenameIndex(
                name: "IX_tb_mesa_Codigo",
                table: "tb_mesas",
                newName: "IX_tb_mesas_Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_mesas",
                table: "tb_mesas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos",
                column: "MesaId",
                principalTable: "tb_mesas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_pedidos_tb_mesas_MesaId",
                table: "tb_pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_mesas",
                table: "tb_mesas");

            migrationBuilder.RenameTable(
                name: "tb_mesas",
                newName: "tb_mesa");

            migrationBuilder.RenameIndex(
                name: "IX_tb_mesas_Codigo",
                table: "tb_mesa",
                newName: "IX_tb_mesa_Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_mesa",
                table: "tb_mesa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_pedidos_tb_mesa_MesaId",
                table: "tb_pedidos",
                column: "MesaId",
                principalTable: "tb_mesa",
                principalColumn: "Id");
        }
    }
}
