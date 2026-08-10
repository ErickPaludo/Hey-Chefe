using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.UnitTests.Domain.Entities
{
    public class UsuarioTest
    {
        private static NomeUsuario NomeValido() => NomeUsuario.Create("Carlos", "Silva");
        private static Email EmailValido() => Email.Create("carlos.silva@email.com");
        private static Senha SenhaValida() => Senha.Create("salt123", "hash123");

        [Fact]
        public void Create_ComDadosValidos_DeveCriarUsuarioComSucesso()
        {
            // Arrange
            var nome = NomeValido();
            var email = EmailValido();
            var senha = SenhaValida();
            var permissao = EPermissaoUsuario.Garcom;

            // Act
            var usuario = Usuario.Create(nome, email, senha, permissao);

            // Assert
            Assert.NotNull(usuario);
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(senha, usuario.Senha);
            Assert.Equal(permissao, usuario.Permissao);
            Assert.Equal(ESituacaoUsuario.Ativo, usuario.Situacao);
        }

        [Fact]
        public void Create_ComNomeNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Usuario.Create(null!, EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.NOME_NULO, exception.Message);
        }

        [Fact]
        public void Create_ComEmailNulo_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Usuario.Create(NomeValido(), null!, SenhaValida(), EPermissaoUsuario.Garcom));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_NULO, exception.Message);
        }

        [Fact]
        public void Create_ComSenhaNula_DeveLancarExcecao()
        {
            // Arrange & Act
            var exception = Record.Exception(() =>
                Usuario.Create(NomeValido(), EmailValido(), null!, EPermissaoUsuario.Garcom));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.SENHA_NULA, exception.Message);
        }

        [Fact]
        public void Create_ComPermissaoInvalida_DeveLancarExcecao()
        {
            // Arrange
            var permissaoInvalida = (EPermissaoUsuario)999;

            // Act
            var exception = Record.Exception(() =>
                Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), permissaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.PERMISSAO_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarNome_ComNomeValido_DeveAtualizarNome()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);
            var novoNome = NomeUsuario.Create("João", "Pedro");

            // Act
            usuario.AtualizarNome(novoNome);

            // Assert
            Assert.Equal(novoNome, usuario.Nome);
        }

        [Fact]
        public void AtualizarNome_ComNomeNulo_DeveLancarExcecao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);

            // Act
            var exception = Record.Exception(() => usuario.AtualizarNome(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.NOME_NULO, exception.Message);
        }

        [Fact]
        public void AtualizarEndereco_ComEmailValido_DeveAtualizarEndereco()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);
            var novoEmail = Email.Create("joao.pedro@email.com");

            // Act
            usuario.AtualizarEndereco(novoEmail);

            // Assert
            Assert.Equal(novoEmail, usuario.Email);
        }

        [Fact]
        public void AtualizarEndereco_ComEmailNulo_DeveLancarExcecao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);

            // Act
            var exception = Record.Exception(() => usuario.AtualizarEndereco(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.EMAIL_NULO, exception.Message);
        }

        [Fact]
        public void AtualizarPermissao_ComPermissaoValida_DeveAtualizarPermissao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);

            // Act
            usuario.AtualizarPermissao(EPermissaoUsuario.Administrador);

            // Assert
            Assert.Equal(EPermissaoUsuario.Administrador, usuario.Permissao);
        }

        [Fact]
        public void AtualizarPermissao_ComPermissaoInvalida_DeveLancarExcecao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);
            var permissaoInvalida = (EPermissaoUsuario)999;

            // Act
            var exception = Record.Exception(() => usuario.AtualizarPermissao(permissaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.PERMISSAO_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoValida_DeveAtualizarSituacao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);

            // Act
            usuario.AtualizarSituacao(ESituacaoUsuario.Inativo);

            // Assert
            Assert.Equal(ESituacaoUsuario.Inativo, usuario.Situacao);
        }

        [Fact]
        public void AtualizarSituacao_ComSituacaoInvalida_DeveLancarExcecao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);
            var situacaoInvalida = (ESituacaoUsuario)999;

            // Act
            var exception = Record.Exception(() => usuario.AtualizarSituacao(situacaoInvalida));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.SITUACAO_INVALIDA, exception.Message);
        }

        [Fact]
        public void AtualizarSenha_ComSenhaValida_DeveAtualizarSenha()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);
            var novaSenha = Senha.Create("salt456", "hash456");

            // Act
            usuario.AtualizarSenha(novaSenha);

            // Assert
            Assert.Equal(novaSenha, usuario.Senha);
        }

        [Fact]
        public void AtualizarSenha_ComSenhaNula_DeveLancarExcecao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);

            // Act
            var exception = Record.Exception(() => usuario.AtualizarSenha(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ExceptionDomain>(exception);
            Assert.Equal(MensagensUsuarios.SENHA_NULA, exception.Message);
        }

        [Fact]
        public void AtualizarSenha_ComSenhaIdentica_DeveLancarExcecao()
        {
            // Arrange
            var usuario = Usuario.Create(NomeValido(), EmailValido(), SenhaValida(), EPermissaoUsuario.Garcom);
            var senhaIdentica = Senha.Create("salt123", "hash123");

            // Act
            var exception = Record.Exception(() => usuario.AtualizarSenha(senhaIdentica));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<UsuariosValidacao>(exception);
            Assert.Equal(MensagensUsuarios.MESMA_SENHA, exception.Message);
        }
    }
}
