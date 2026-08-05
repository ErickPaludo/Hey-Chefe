using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Entidades.Usuarios.Enums;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.Domain.Entidades.Usuarios
{
    public sealed class Usuario : EntidadeBase
    {
        public Nome Nome { get; private set; }
        public Email Endereco { get; private set; }
        public Senha Senha { get; private set; }
        public EPermissaoUsuario Permissao { get; private set; }
        public ESituacaoUsuario Situacao { get; private set; }
        private Usuario(Nome nome, Email endereco, Senha senha,EPermissaoUsuario permissao)
        {
            ValidaNulo.Verifica(nome, MensagensUsuarios.NOME_NULO);
            ValidaNulo.Verifica(endereco, MensagensUsuarios.EMAIL_NULO);
            ValidaNulo.Verifica(senha, MensagensUsuarios.SENHA_NULA);

            ValidaPermissao(permissao);

            Nome = nome;
            Endereco = endereco;
            Senha = senha;
            Permissao = permissao;
            Situacao = ESituacaoUsuario.Ativo;
        }
        private void ValidaSituacao(ESituacaoUsuario situacao) => 
            UsuariosValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoUsuario), situacao), MensagensUsuarios.SITUACAO_INVALIDA);
        
        private void ValidaPermissao(EPermissaoUsuario permissao) =>
            UsuariosValidacao.Verifica(!Enum.IsDefined(typeof(EPermissaoUsuario), permissao), MensagensUsuarios.PERMISSAO_INVALIDA);
        
        public static Usuario Create(Nome nome, Email endereco, Senha senha,EPermissaoUsuario permissao) => 
            new Usuario(nome, endereco, senha,permissao);

        #region Atualiza
        public void AtualizarNome(Nome nome)
        {
            Nome = nome;
        }

        public void AtualizarEndereco(Email endereco)
        {
            Endereco = endereco;
        }

        public void AtualizarPermissao(EPermissaoUsuario permissao)
        {
            ValidaPermissao(permissao);
            Permissao = permissao;
        }

        public void AtualizarSituacao(ESituacaoUsuario situacao)
        {
            ValidaSituacao(situacao);
            Situacao = situacao;
        }
        #endregion
    }
}
