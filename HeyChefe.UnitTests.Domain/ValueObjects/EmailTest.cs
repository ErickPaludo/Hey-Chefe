using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.UnitTests.Domain.ValueObjects
{
    public class EmailTest
    {
        [Fact]
        public void Create_ComEmailValido_DeveCriarEmailComSucesso()
        {
            // Arrange & Act
            var email = Email.Create("carlos.silva@email.com");

            // Assert
            Assert.NotNull(email);
            Assert.Equal("carlos.silva@email.com", email.Endereco);
        }

        [Fact]
        public void Create_ComEspacosAoRedor_DeveRemoverEspacos()
        {
            // Arrange & Act
            var email = Email.Create("  carlos.silva@email.com  ");

            // Assert
            Assert.Equal("carlos.silva@email.com", email.Endereco);
        }

        [Fact]
        public void Create_ComEmailNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void Create_ComEmailVazio_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create(""));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_OBRIGATORIO, exception.Message);
        }

        [Fact]
        public void Create_ComEmailContendoEspaco_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create("carlos silva@email.com"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_INVALIDO, exception.Message);
        }

        [Fact]
        public void Create_ComEmailMuitoCurto_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create("a@b.c"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_MINIMO, exception.Message);
        }

        [Fact]
        public void Create_ComEmailSemFormatoValido_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => Email.Create("email-invalido"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_INVALIDO, exception.Message);
        }

        [Fact]
        public void InstanciasComMesmoEndereco_DeveCompararPorIgualdade()
        {
            // Arrange & Act
            var email1 = Email.Create("carlos.silva@email.com");
            var email2 = Email.Create("carlos.silva@email.com");

            // Assert
            Assert.Equal(email1, email2);
        }
    }
}
