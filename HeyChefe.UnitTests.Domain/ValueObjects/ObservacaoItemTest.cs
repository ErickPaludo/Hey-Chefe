using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class ObservacaoItemTest
    {
        [Fact]
        public void Create_ComTextoValido_DeveCriarObservacaoComSucesso()
        {
            // Arrange & Act
            var observacao = ObservacaoItem.Create("Sem cebola");

            // Assert
            Assert.NotNull(observacao);
            Assert.Equal("Sem cebola", observacao.Texto);
        }

        [Fact]
        public void Create_ComTextoNoLimiteMaximo_DeveCriarObservacaoComSucesso()
        {
            // Arrange
            var textoNoLimite = new string('A', 400);

            // Act
            var observacao = ObservacaoItem.Create(textoNoLimite);

            // Assert
            Assert.NotNull(observacao);
            Assert.Equal(textoNoLimite, observacao.Texto);
        }

        [Fact]
        public void Create_ComTextoNulo_DeveCriarObservacaoComTextoNulo()
        {
            // Arrange & Act
            var observacao = ObservacaoItem.Create(null!);

            // Assert
            Assert.NotNull(observacao);
            Assert.Null(observacao.Texto);
        }

        [Fact]
        public void Create_ComTextoVazio_DeveCriarObservacaoComTextoVazio()
        {
            // Arrange & Act
            var observacao = ObservacaoItem.Create("");

            // Assert
            Assert.NotNull(observacao);
            Assert.Equal("", observacao.Texto);
        }

        [Fact]
        public void Create_ComTextoAlemDoLimiteMaximo_DeveLancarExcecao()
        {
            // Arrange
            var textoAlemDoLimite = new string('A', 401);

            // Act
            var exception = Record.Exception(() => ObservacaoItem.Create(textoAlemDoLimite));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagensBase.OBSERVACAO_TAMANHO_INVALIDO(400), exception.Message);
        }

        [Fact]
        public void InstanciasComMesmoTexto_DeveCompararPorIgualdade()
        {
            // Arrange & Act
            var observacao1 = ObservacaoItem.Create("Sem cebola");
            var observacao2 = ObservacaoItem.Create("Sem cebola");

            // Assert
            Assert.Equal(observacao1, observacao2);
        }
    }
}
