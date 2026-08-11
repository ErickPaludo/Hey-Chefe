using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Itens.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;
using HeyChefe.Domain.Validacoes.Item.Mensagens;

namespace HeyChefe.UnitTests.Domain.Entities
{
    public class ItemTest
    {
        private static Codigo CodigoValido() => Codigo.Create(1);
        private static TituloItem NomeValido() => TituloItem.Create("X-Burger");
        private static ObservacaoItem DescricaoValida() => ObservacaoItem.Create("Sem cebola");
        private static Saldo PrecoVendaValido() => Saldo.Create(25m);
        private static Saldo MargemLucroValida() => Saldo.Create(10m);
        private static Categoria CategoriaValida() => Categoria.Create(Codigo.Create(1), TituloCategoria.Create("Lanches"), Cor.Create("#FF5733"));

        private static Item ItemValido() =>
            Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), PrecoVendaValido(), MargemLucroValida(), null);

        [Fact]
        public void Create_ComDadosValidos_DeveCriarItemComSucesso()
        {
            // Arrange
            var codigo = CodigoValido();
            var descricao = DescricaoValida();
            var nome = NomeValido();
            var precoVenda = PrecoVendaValido();
            var margemLucro = MargemLucroValida();

            // Act
            var item = Item.Create(codigo, descricao, nome, precoVenda, margemLucro, null);

            // Assert
            Assert.NotNull(item);
            Assert.Equal(codigo, item.Codigo);
            Assert.Equal(descricao, item.Descricao);
            Assert.Equal(nome, item.Nome);
            Assert.Equal(precoVenda, item.PrecoCusto);
            Assert.Equal(margemLucro, item.MargemLucro);
            Assert.Null(item.Categoria);
            Assert.Equal(ESituacaoItem.Ativo, item.Situacao);
        }

        [Fact]
        public void Create_ComCategoriaValida_DeveDefinirCategoria()
        {
            // Arrange
            var categoria = CategoriaValida();

            // Act
            var item = Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), PrecoVendaValido(), MargemLucroValida(), categoria);

            // Assert
            Assert.NotNull(item);
            Assert.Equal(categoria, item.Categoria);
        }

        [Fact]
        public void Create_SemDescricao_DeveCriarItemComDescricaoNula()
        {
            // Arrange & Act
            var item = Item.Create(CodigoValido(), null, NomeValido(), PrecoVendaValido(), MargemLucroValida(), null);

            // Assert
            Assert.NotNull(item);
            Assert.Null(item.Descricao);
        }

        [Fact]
        public void Create_SemCategoria_DeveCriarItemComCategoriaNula()
        {
            // Arrange & Act
            var item = Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), PrecoVendaValido(), MargemLucroValida(), null);

            // Assert
            Assert.NotNull(item);
            Assert.Null(item.Categoria);
        }

        [Fact]
        public void Create_ComMargemLucroZero_DeveCriarItemComSucesso()
        {
            // Arrange & Act
            var item = Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), PrecoVendaValido(), Saldo.Create(0m), null);

            // Assert
            Assert.NotNull(item);
            Assert.Equal(Saldo.Create(0m), item.MargemLucro);
        }

        [Fact]
        public void Create_ComCodigoNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Item.Create(null!, DescricaoValida(), NomeValido(), PrecoVendaValido(), MargemLucroValida(), null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.CODIGO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void Create_ComNomeNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Item.Create(CodigoValido(), DescricaoValida(), null!, PrecoVendaValido(), MargemLucroValida(), null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.CODIGO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void Create_ComPrecoVendaNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), null!, MargemLucroValida(), null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.PRECO_VENDA_NULO, exception.Message);
        }

        [Fact]
        public void Create_ComMargemLucroNula_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), PrecoVendaValido(), null!, null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.MARGEM_LUCRO_NULA, exception.Message);
        }

        [Fact]
        public void Create_ComPrecoVendaZero_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), Saldo.Create(0m), MargemLucroValida(), null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagemItem.VALOR_INVALIDO, exception.Message);
        }

        [Fact]
        public void Create_ComPrecoVendaNegativo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), Saldo.Create(-5m), MargemLucroValida(), null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagemItem.VALOR_INVALIDO, exception.Message);
        }

        [Fact]
        public void Create_ComMargemLucroNegativa_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), PrecoVendaValido(), Saldo.Create(-1m), null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagemItem.MARGEM_LUCRO_INVALIDA, exception.Message);
        }

        [Fact]
        public void PrecoFinal_DeveCalcularPrecoVendaAcrescidoDaMargemLucro()
        {
            // Arrange
            var item = Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), Saldo.Create(100m), Saldo.Create(10m), null);

            // Act
            var precoFinal = item.PrecoFinal;

            // Assert
            Assert.NotNull(precoFinal);
            Assert.Equal(100.1m, precoFinal.Valor);
        }

        [Fact]
        public void PrecoFinal_ComMargemLucroZero_DeveRetornarPrecoCusto()
        {
            // Arrange
            var item = Item.Create(CodigoValido(), DescricaoValida(), NomeValido(), Saldo.Create(100m), Saldo.Create(0m), null);

            // Act
            var precoFinal = item.PrecoFinal;

            // Assert
            Assert.NotNull(precoFinal);
            Assert.Equal(100m, precoFinal.Valor);
        }

        [Fact]
        public void AtualizarNome_ComNomeValido_DeveAtualizarNome()
        {
            // Arrange
            var item = ItemValido();
            var novoNome = TituloItem.Create("X-Salada");

            // Act
            item.AtualizarNome(novoNome);

            // Assert
            Assert.Equal(novoNome, item.Nome);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarNome_ComNomeNulo_DeveLancarExcecao()
        {
            // Arrange
            var item = ItemValido();

            // Act
            var exception = Record.Exception(() => item.AtualizarNome(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.CODIGO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void AtualizarDescricao_ComDescricaoValida_DeveAtualizarDescricao()
        {
            // Arrange
            var item = Item.Create(CodigoValido(), null, NomeValido(), PrecoVendaValido(), MargemLucroValida(), null);
            var novaDescricao = ObservacaoItem.Create("Com bacon extra");

            // Act
            item.AtualizarDescricao(novaDescricao);

            // Assert
            Assert.Equal(novaDescricao, item.Descricao);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarDescricao_ComDescricaoNula_DeveDefinirDescricaoNula()
        {
            // Arrange
            var item = ItemValido();

            // Act
            item.AtualizarDescricao(null);

            // Assert
            Assert.Null(item.Descricao);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarPrecoVenda_ComPrecoValido_DeveAtualizarPrecoVenda()
        {
            // Arrange
            var item = ItemValido();
            var novoPreco = Saldo.Create(30m);

            // Act
            item.AtualizarPrecoVenda(novoPreco);

            // Assert
            Assert.Equal(novoPreco, item.PrecoCusto);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarPrecoVenda_ComPrecoNulo_DeveLancarExcecao()
        {
            // Arrange
            var item = ItemValido();

            // Act
            var exception = Record.Exception(() => item.AtualizarPrecoVenda(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.PRECO_VENDA_NULO, exception.Message);
        }

        [Fact]
        public void AtualizarPrecoVenda_ComPrecoZero_DeveLancarExcecao()
        {
            // Arrange
            var item = ItemValido();

            // Act
            var exception = Record.Exception(() => item.AtualizarPrecoVenda(Saldo.Create(0m)));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagemItem.VALOR_INVALIDO, exception.Message);
        }

        [Fact]
        public void AtualizarPrecoVenda_ComPrecoNegativo_DeveLancarExcecao()
        {
            // Arrange
            var item = ItemValido();

            // Act
            var exception = Record.Exception(() => item.AtualizarPrecoVenda(Saldo.Create(-5m)));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagemItem.VALOR_INVALIDO, exception.Message);
        }

        [Fact]
        public void AtualizarMargemLucro_ComMargemValida_DeveAtualizarMargemLucro()
        {
            // Arrange
            var item = ItemValido();
            var novaMargem = Saldo.Create(15m);

            // Act
            item.AtualizarMargemLucro(novaMargem);

            // Assert
            Assert.Equal(novaMargem, item.MargemLucro);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarMargemLucro_ComMargemNula_DeveLancarExcecao()
        {
            // Arrange
            var item = ItemValido();

            // Act
            var exception = Record.Exception(() => item.AtualizarMargemLucro(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.MARGEM_LUCRO_NULA, exception.Message);
        }

        [Fact]
        public void AtualizarMargemLucro_ComMargemNegativa_DeveLancarExcecao()
        {
            // Arrange
            var item = ItemValido();

            // Act
            var exception = Record.Exception(() => item.AtualizarMargemLucro(Saldo.Create(-2m)));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagemItem.MARGEM_LUCRO_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarCategoria_ComCategoriaValida_DeveAtualizarCategoria()
        {
            // Arrange
            var item = ItemValido();
            var categoria = CategoriaValida();

            // Act
            item.AtualizarCategoria(categoria);

            // Assert
            Assert.Equal(categoria, item.Categoria);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarCategoria_ComCategoriaNula_DeveDefinirCategoriaNula()
        {
            // Arrange
            var item = ItemValido();
            item.AtualizarCategoria(CategoriaValida());

            // Act
            item.AtualizarCategoria(null);

            // Assert
            Assert.Null(item.Categoria);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoValida_DeveAtualizarSituacao()
        {
            // Arrange
            var item = ItemValido();

            // Act
            item.AtualizarSituacao(ESituacaoItem.Inativo);

            // Assert
            Assert.Equal(ESituacaoItem.Inativo, item.Situacao);
            Assert.NotNull(item.DataHoraAlteracao);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoInvalida_DeveLancarExcecao()
        {
            // Arrange
            var item = ItemValido();
            var situacaoInvalida = (ESituacaoItem)999;

            // Act
            var exception = Record.Exception(() => item.AtualizarSituacao(situacaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagensBase.SITUACAO_INVALIDA, exception.Message);
        }
    }
}
