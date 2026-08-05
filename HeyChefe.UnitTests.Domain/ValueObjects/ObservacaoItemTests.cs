using HeyChefe.Domain.Objetos_de_Valor.Observação;
using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class ObservacaoItemTests
    {
        [Fact]
        public void Create_ComTextoValido_DeveCriarObservacaoComSucesso()
        {
            // Arrange
            string texto = "Sem cebola, por favor.";

            // Act
            var observacao = ObservacaoItem.Create(texto);

            // Assert
            Assert.NotNull(observacao);
            Assert.Equal(texto, observacao.Texto);
        }

        [Fact]
        public void Create_ComEspacosNasExtremidades_DeveRemoverEspacos()
        {
            // Arrange
            string texto = "  Sem cebola  ";

            // Act
            var observacao = ObservacaoItem.Create(texto);

            // Assert
            Assert.Equal("Sem cebola", observacao.Texto);
        }

        [Fact]
        public void Create_ComTextoVazio_DeveCriarObservacaoComTextoVazio()
        {
            // Arrange
            string texto = string.Empty;

            // Act
            var observacao = ObservacaoItem.Create(texto);

            // Assert
            // Como texto vazio satisfaz string.IsNullOrEmpty, o trim é pulado,
            // mas a validação de tamanho (0 > 400) não é violada.
            Assert.Equal(string.Empty, observacao.Texto);
        }

        [Fact]
        public void Create_ComTextoApenasComEspacos_DeveResultarEmTextoVazio()
        {
            // Arrange
            string texto = "   ";

            // Act
            var observacao = ObservacaoItem.Create(texto);

            // Assert
            // Como "   " não é IsNullOrEmpty, o texto passa pelo trim antes da
            // validação, resultando em string vazia.
            Assert.Equal(string.Empty, observacao.Texto);
        }

        [Fact]
        public void Create_ComTextoMaiorQueOMaximo_DeveLancarExcecao()
        {
            // Arrange
            string textoLongo = new string('a', 401);

            // Act
            var exception = Record.Exception(() => ObservacaoItem.Create(textoLongo));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ItemValidacao>(exception);
            Assert.Equal(MensagensBase.OBSERVACAO_TAMANHO_INVALIDO(400), exception.Message);
        }

        [Fact]
        public void Create_ComTextoNoTamanhoMaximoPermitido_DeveCriarComSucesso()
        {
            // Arrange
            string textoLimite = new string('a', 400); // 400 caracteres

            // Act
            var observacao = ObservacaoItem.Create(textoLimite);

            // Assert
            Assert.Equal(400, observacao.Texto.Length);
        }

        [Fact]
        public void Create_ComConstrutorPublico_DeveCriarObservacaoComSucesso()
        {
            // Arrange
            string texto = "Bem passado.";

            // Act
            // ObservacaoItem expõe um construtor público, diferente da maioria
            // dos outros Value Objects, que usam construtor privado + Create.
            var observacao = new ObservacaoItem(texto);

            // Assert
            Assert.Equal(texto, observacao.Texto);
        }

        [Fact]
        public void Equals_ComMesmoTexto_DevemSerIguais()
        {
            // Arrange
            var observacao1 = ObservacaoItem.Create("Sem cebola");
            var observacao2 = ObservacaoItem.Create("Sem cebola");

            // Act & Assert
            Assert.Equal(observacao1, observacao2);
            Assert.True(observacao1 == observacao2);
        }

        [Fact]
        public void Equals_ComTextosDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var observacao1 = ObservacaoItem.Create("Sem cebola");
            var observacao2 = ObservacaoItem.Create("Sem tomate");

            // Act & Assert
            Assert.NotEqual(observacao1, observacao2);
            Assert.False(observacao1 == observacao2);
        }
    }
}
