using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TabelaAutenticacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autenticacao",
                columns: table => new
                {
                    IdSession = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationRefresh = table.Column<long>(type: "bigint", nullable: false),
                    Revoke = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autenticacao", x => x.IdSession);
                    table.ForeignKey(
                        name: "FK_Autenticacao_tb_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autenticacao_UsuarioId",
                table: "Autenticacao",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autenticacao");
        }
    }
}
