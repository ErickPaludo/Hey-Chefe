using HeyChefe.Domain.Entidades.Segurança;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;

namespace HeyChefe.UnitTests.Domain.Entities
{
    public class AutenticacaoTest
    {
        private static Usuario UsuarioValido() => Usuario.Create(
            NomeUsuario.Create("Carlos", "Silva"),
            Email.Create("carlos.silva@email.com"),
            Senha.Create("salt123", "hash123"),
            EPermissaoUsuario.Garcom);

        private static Autenticacao AutenticacaoValida() => new Autenticacao(
            UsuarioValido(),
            "refresh-token",
            1700000000);

        [Fact]
        public void Construtor_ComDadosValidos_DeveCriarAutenticacaoComSucesso()
        {
            // Arrange
            var usuario = UsuarioValido();
            var refreshToken = "refresh-token";
            long expirationRefresh = 1700000000;

            // Act
            var autenticacao = new Autenticacao(usuario, refreshToken, expirationRefresh);

            // Assert
            Assert.NotNull(autenticacao);
            Assert.Equal(usuario, autenticacao.Usuario);
            Assert.Equal(refreshToken, autenticacao.RefreshToken);
            Assert.Equal(expirationRefresh, autenticacao.ExpirationRefresh);
            Assert.False(autenticacao.Revoke);
        }

        [Fact]
        public void Construtor_ComUsuarioNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => new Autenticacao(null!, "refresh-token", 1700000000));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.USUARIO_NULO, exception.Message);
        }

        [Fact]
        public void Construtor_ComRefreshTokenNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() => new Autenticacao(UsuarioValido(), null!, 1700000000));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.REFRESH_TOKEN_NULO, exception.Message);
        }

        [Fact]
        public void Construtor_DeveGerarIdSessionValido()
        {
            // Arrange & Act
            var autenticacao = AutenticacaoValida();

            // Assert
            Assert.NotNull(autenticacao.IdSession);
            Assert.NotEmpty(autenticacao.IdSession);
            Assert.True(Guid.TryParse(autenticacao.IdSession, out _));
        }

        [Fact]
        public void AtualizaRefreshToken_ComDadosValidos_DeveAtualizarToken()
        {
            // Arrange
            var autenticacao = AutenticacaoValida();
            var novoToken = "novo-refresh-token";
            long novaExpiracao = 1800000000;

            // Act
            autenticacao.AtualizaRefreshToken(novoToken, novaExpiracao);

            // Assert
            Assert.Equal(novoToken, autenticacao.RefreshToken);
            Assert.Equal(novaExpiracao, autenticacao.ExpirationRefresh);
            Assert.False(autenticacao.Revoke);
        }

        [Fact]
        public void AtualizaRefreshToken_AposRevogacao_DeveCancelarRevogacao()
        {
            // Arrange
            var autenticacao = AutenticacaoValida();
            autenticacao.RevokaToken();
            Assert.True(autenticacao.Revoke);

            // Act
            autenticacao.AtualizaRefreshToken("novo-refresh-token", 1800000000);

            // Assert
            Assert.False(autenticacao.Revoke);
        }

        [Fact]
        public void AtualizaRefreshToken_ComRefreshTokenNulo_DeveLancarExcecao()
        {
            // Arrange
            var autenticacao = AutenticacaoValida();

            // Act
            var exception = Record.Exception(() => autenticacao.AtualizaRefreshToken(null!, 1700000000));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensBase.REFRESH_TOKEN_NULO, exception.Message);
        }

        [Fact]
        public void RevokaToken_DeveMarcarTokenComoRevogado()
        {
            // Arrange
            var autenticacao = AutenticacaoValida();

            // Act
            autenticacao.RevokaToken();

            // Assert
            Assert.True(autenticacao.Revoke);
        }
    }
}
