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
        public NomeUsuario Nome { get; private set; }
        public Email Endereco { get; private set; }
        public Senha Senha { get; private set; }
        public EPermissaoUsuario Permissao { get; private set; }
        public ESituacaoUsuario Situacao { get; private set; }
        public Usuario() { }
        private Usuario(NomeUsuario nome, Email endereco, Senha senha, EPermissaoUsuario permissao)
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
        public static Usuario Create(NomeUsuario nome, Email endereco, Senha senha, EPermissaoUsuario permissao) =>
            new Usuario(nome, endereco, senha, permissao);

        #region Atualiza
        public void AtualizarNome(NomeUsuario nome)
        {
            ValidaNulo.Verifica(nome, MensagensUsuarios.NOME_NULO);
            Nome = nome;
        }

        public void AtualizarEndereco(Email endereco)
        {
            ValidaNulo.Verifica(endereco, MensagensUsuarios.EMAIL_NULO);
            Endereco = endereco;
        }

        public void AtualizarPermissao(EPermissaoUsuario permissao)
        {
            ValidaPermissao(permissao);
            Permissao = permissao;
        }

        public void AtualizarSituacao(ESituacaoUsuario situacao)
        {
            UsuariosValidacao.Verifica(!Enum.IsDefined(typeof(ESituacaoUsuario), situacao), MensagensUsuarios.SITUACAO_INVALIDA);
            Situacao = situacao;
        }

        public void AtualizarSenha(Senha senha)
        {
            ValidaNulo.Verifica(senha, MensagensUsuarios.SENHA_NULA);
            Senha.AtualizaSenha(senha);
            Senha = senha;
        }
        #endregion
        private void ValidaPermissao(EPermissaoUsuario permissao)
        {
            ValidaNulo.Verifica(permissao, MensagensUsuarios.PERMISSAO_NULA);
            UsuariosValidacao.Verifica(!Enum.IsDefined(typeof(EPermissaoUsuario), permissao), MensagensUsuarios.PERMISSAO_INVALIDA);
        }

    }
}
