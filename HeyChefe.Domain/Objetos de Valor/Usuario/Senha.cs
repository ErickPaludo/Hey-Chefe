using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.Domain.Objetos_de_Valor
{
    public sealed record class Senha
    {
        public string Salt { get; }
        public string Hash { get; }

        private Senha(string salt, string hash)
        {
            Salt = Preparar(salt);
            Hash = Preparar(hash);
        }

        public static Senha Create(string salt, string hash)
        {
            return new Senha(salt, hash);
        }

        public void AtualizaSenha(Senha senha)
        {
            ValidaNulo.Verifica(senha, MensagensUsuarios.SENHA_NULA);
            UsuariosValidacao.Verifica(this == senha, MensagensUsuarios.MESMA_SENHA);
        }
        private static string Preparar(string valor)
        {
            UsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(valor), MensagensUsuarios.SENHA_VAZIA);
            return valor.Trim();
        }
    }
}
