using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeyChefe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class itensAndcategorias : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_itens");

            migrationBuilder.DropTable(
                name: "tb_categorias");
        }
    }
}
