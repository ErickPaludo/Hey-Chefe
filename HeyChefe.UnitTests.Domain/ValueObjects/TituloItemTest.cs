using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class TituloItemTest
    {
        [Fact]
        public void Create_ComTextoValido_DeveCriarTituloComSucesso()
        {
            // Arrange & Act
            var titulo = TituloItem.Create("X-Burger");

            // Assert
            Assert.NotNull(titulo);
            Assert.Equal("X-Burger", titulo.Texto);
        }

        [Fact]
        public void Create_ComTextoMinusculo_DeveConverterParaTitleCase()
        {
            // Arrange & Act
            var titulo = TituloItem.Create("x-burger");

            // Assert
            Assert.Equal("X-Burger", titulo.Texto);
        }

        [Fact]
        public void Create_ComEspacosAoRedor_DeveRemoverEspacos()
        {
            // Arrange & Act
            var titulo = TituloItem.Create("  X-Burger  ");

            // Assert
            Assert.Equal("X-Burger", titulo.Texto);
        }

        [Fact]
        public void Create_ComTextoNoLimiteMaximo_DeveCriarTituloComSucesso()
        {
            // Arrange & Act
            var titulo = TituloItem.Create(new string('A', 30));

            // Assert
            Assert.NotNull(titulo);
            Assert.Equal(30, titulo.Texto.Length);
        }

        [Fact]
        public void Create_ComTextoNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => TituloItem.Create(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.TITULO_NULO, exception.Message);
        }

        [Fact]
        public void Create_ComTextoMuitoCurto_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => TituloItem.Create("A"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagensBase.TITULO_TAMANHO_INVALIDO(2, 30), exception.Message);
        }

        [Fact]
        public void Create_ComTextoMuitoLongo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => TituloItem.Create(new string('A', 31)));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagensBase.TITULO_TAMANHO_INVALIDO(2, 30), exception.Message);
        }

        [Fact]
        public void InstanciasComMesmoTexto_DeveCompararPorIgualdade()
        {
            // Arrange & Act
            var titulo1 = TituloItem.Create("X-Burger");
            var titulo2 = TituloItem.Create("X-Burger");

            // Assert
            Assert.Equal(titulo1, titulo2);
        }
    }
}
