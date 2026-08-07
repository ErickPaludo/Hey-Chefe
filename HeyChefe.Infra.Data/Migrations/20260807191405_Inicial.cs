using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_mesa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situação do usuário: 0-Disponivel | 1-Ocupada | 2-LimpezaPendente |3-Reservada"),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_mesa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permissao = table.Column<int>(type: "int", nullable: false, comment: "Permissões do usuário: 0-Administrador | 1-Garçom | 2-Cozinha"),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situação do usuário: 0-Ativo | 1-Inativo"),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_itens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MargemLucro = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situação do usuário: 0-Ativo | 1-Inativo | 2-Excluído |3-Sem Estoque"),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_itens_tb_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "tb_categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroPedido = table.Column<int>(type: "int", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situação do usuário: 0-Pendente | 1-Iniciado | 2-Concluido |3-Cancelado"),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fechamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MesaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_pedidos_tb_mesa_MesaId",
                        column: x => x.MesaId,
                        principalTable: "tb_mesa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_pedidos_tb_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tb_usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_linhas_pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, comment: "Situação do usuário: 0-Pendente | 1-Pronto | 2-Concluido |3-Cancelado"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Cortesia = table.Column<bool>(type: "bit", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_linhas_pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_linhas_pedidos_tb_itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "tb_itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_linhas_pedidos_tb_pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "tb_pedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_categorias_Codigo",
                table: "tb_categorias",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_itens_CategoriaId",
                table: "tb_itens",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_itens_Codigo",
                table: "tb_itens",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_linhas_pedidos_ItemId",
                table: "tb_linhas_pedidos",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_linhas_pedidos_PedidoId",
                table: "tb_linhas_pedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_mesa_Codigo",
                table: "tb_mesa",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedidos_MesaId",
                table: "tb_pedidos",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedidos_NumeroPedido",
                table: "tb_pedidos",
                column: "NumeroPedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedidos_UsuarioId",
                table: "tb_pedidos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_linhas_pedidos");

            migrationBuilder.DropTable(
                name: "tb_itens");

            migrationBuilder.DropTable(
                name: "tb_pedidos");

            migrationBuilder.DropTable(
                name: "tb_categorias");

            migrationBuilder.DropTable(
                name: "tb_mesa");

            migrationBuilder.DropTable(
                name: "tb_usuarios");
        }
    }
}
