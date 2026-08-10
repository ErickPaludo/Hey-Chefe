using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TabelaAutenticacaoRenomeada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autenticacao_tb_usuarios_UsuarioId",
                table: "Autenticacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Autenticacao",
                table: "Autenticacao");

            migrationBuilder.RenameTable(
                name: "Autenticacao",
                newName: "tb_autenticacao");

            migrationBuilder.RenameIndex(
                name: "IX_Autenticacao_UsuarioId",
                table: "tb_autenticacao",
                newName: "IX_tb_autenticacao_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_autenticacao",
                table: "tb_autenticacao",
                column: "IdSession");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao",
                column: "UsuarioId",
                principalTable: "tb_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_autenticacao_tb_usuarios_UsuarioId",
                table: "tb_autenticacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_autenticacao",
                table: "tb_autenticacao");

            migrationBuilder.RenameTable(
                name: "tb_autenticacao",
                newName: "Autenticacao");

            migrationBuilder.RenameIndex(
                name: "IX_tb_autenticacao_UsuarioId",
                table: "Autenticacao",
                newName: "IX_Autenticacao_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Autenticacao",
                table: "Autenticacao",
                column: "IdSession");

            migrationBuilder.AddForeignKey(
                name: "FK_Autenticacao_tb_usuarios_UsuarioId",
                table: "Autenticacao",
                column: "UsuarioId",
                principalTable: "tb_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
