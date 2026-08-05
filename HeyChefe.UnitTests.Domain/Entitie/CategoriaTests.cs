using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.Entitie
{
    public class CategoriaTests
    {
        private static Codigo CodigoValido() => Codigo.Create(1);
        private static TituloCategoria TituloValido() => TituloCategoria.Create("Bebidas");
        private static Cor CorValida() => Cor.Create("#FFFFFF");

        [Fact]
        public void Create_ComDadosValidos_DeveCriarCategoriaComSucesso()
        {
            // Arrange
            var codigo = CodigoValido();
            var titulo = TituloValido();
            var cor = CorValida();

            // Act
            var categoria = Categoria.Create(codigo, titulo, cor);

            // Assert
            Assert.NotNull(categoria);
            Assert.Equal(codigo, categoria.Codigo);
            Assert.Equal(titulo, categoria.Titulo);
            Assert.Equal(cor, categoria.Cor);
        }

        [Fact]
        public void Create_ComCodigoNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Categoria.Create(null!, TituloValido(), CorValida()));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.CODIGO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void Create_ComTituloNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Categoria.Create(CodigoValido(), null!, CorValida()));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.TITULO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void Create_ComCorNula_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Categoria.Create(CodigoValido(), TituloValido(), null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.COR_OBRIGATORIA, exception.Message);
        }

        [Fact]
        public void AlterarTitulo_ComTituloValido_DeveAtualizarTitulo()
        {
            // Arrange
            var categoria = Categoria.Create(CodigoValido(), TituloValido(), CorValida());
            var novoTitulo = TituloCategoria.Create("Sobremesas");

            // Act
            categoria.AlterarTitulo(novoTitulo);

            // Assert
            Assert.Equal(novoTitulo, categoria.Titulo);
        }

        [Fact]
        public void AlterarTitulo_ComTituloNulo_DeveLancarExcecao()
        {
            // Arrange
            var categoria = Categoria.Create(CodigoValido(), TituloValido(), CorValida());

            // Act
            var exception = Record.Exception(() => categoria.AlterarTitulo(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.TITULO_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void AlterarCor_ComCorValida_DeveAtualizarCor()
        {
            // Arrange
            var categoria = Categoria.Create(CodigoValido(), TituloValido(), CorValida());
            var novaCor = Cor.Create("#000000");

            // Act
            categoria.AlterarCor(novaCor);

            // Assert
            Assert.Equal(novaCor, categoria.Cor);
        }

        [Fact]
        public void AlterarCor_ComCorNula_DeveLancarExcecao()
        {
            // Arrange
            var categoria = Categoria.Create(CodigoValido(), TituloValido(), CorValida());

            // Act
            var exception = Record.Exception(() => categoria.AlterarCor(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.COR_OBRIGATORIA, exception.Message);
        }
    }
}
