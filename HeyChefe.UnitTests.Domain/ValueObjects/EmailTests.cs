using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Create_ComEnderecoValido_DeveCriarEmailComSucesso()
        {
            // Arrange
            string enderecoValido = "carlos.silva@email.com";

            // Act
            var email = Email.Create(enderecoValido);

            // Assert
            Assert.NotNull(email);
            Assert.Equal(enderecoValido, email.Endereco);
        }

        [Fact]
        public void Create_ComEspacosNasExtremidades_DeveRemoverEspacos()
        {
            // Arrange
            string enderecoComEspacos = "  carlos.silva@email.com  ";

            // Act
            var email = Email.Create(enderecoComEspacos);

            // Assert
            Assert.Equal("carlos.silva@email.com", email.Endereco);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ComEnderecoNuloOuVazio_DeveLancarExcecao(string? enderecoInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create(enderecoInvalido!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_OBRIGATORIO, exception.Message);
        }

        [Theory]
        [InlineData("carlos silva@email.com")]
        [InlineData("carlos.silva@ email.com")]
        [InlineData("carlos.silva @email.com")]
        public void Create_ComEnderecoContendoEspacoInterno_DeveLancarExcecao(string enderecoInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create(enderecoInvalido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_INVALIDO, exception.Message);
        }

        [Theory]
        [InlineData("a@b.c")]
        [InlineData("ab@c")]
        public void Create_ComEnderecoMenorQueOMinimo_DeveLancarExcecao(string enderecoCurto)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create(enderecoCurto));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_MINIMO, exception.Message);
        }

        [Fact]
        public void Create_ComEnderecoMaiorQueOMaximo_DeveLancarExcecao()
        {
            // Arrange
            string localPart = new string('a', 245);
            string enderecoLongo = $"{localPart}@example.com"; // 257 caracteres

            // Act
            var exception = Record.Exception(() => Email.Create(enderecoLongo));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_MAXIMO, exception.Message);
        }

        [Theory]
        [InlineData("carlossilva")]
        [InlineData("carlos.silva@")]
        [InlineData("@email.com")]
        [InlineData("carlos..silva@email.com")]
        public void Create_ComFormatoInvalido_DeveLancarExcecao(string enderecoInvalido)
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create(enderecoInvalido));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_INVALIDO, exception.Message);
        }

        [Fact]
        public void Create_ComEnderecoNoTamanhoMinimoPermitido_DeveCriarComSucesso()
        {
            // Arrange
            string enderecoLimite = "a@b.co"; // 6 caracteres

            // Act
            var email = Email.Create(enderecoLimite);

            // Assert
            Assert.Equal(enderecoLimite, email.Endereco);
        }

        [Fact]
        public void Create_ComEnderecoNoTamanhoMaximoPermitido_DeveCriarComSucesso()
        {
            // Arrange
            string localPart = new string('a', 244);
            string enderecoLimite = $"{localPart}@example.com"; // 256 caracteres

            // Act
            var email = Email.Create(enderecoLimite);

            // Assert
            Assert.Equal(enderecoLimite, email.Endereco);
            Assert.Equal(256, email.Endereco.Length);
        }

        [Fact]
        public void Equals_ComMesmoEndereco_DevemSerIguais()
        {
            // Arrange
            var email1 = Email.Create("carlos.silva@email.com");
            var email2 = Email.Create("carlos.silva@email.com");

            // Act & Assert
            Assert.Equal(email1, email2);
            Assert.True(email1 == email2);
        }

        [Fact]
        public void Equals_ComEnderecosDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var email1 = Email.Create("carlos.silva@email.com");
            var email2 = Email.Create("joao.pedro@email.com");

            // Act & Assert
            Assert.NotEqual(email1, email2);
            Assert.False(email1 == email2);
        }
    }
}
