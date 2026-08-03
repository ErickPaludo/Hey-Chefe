    using Financ.Domain.Validacoes.Usuarios;
    using Financ.Domain.Validacoes.Usuarios.Mensagens;

    namespace Financ.Domain.Objetos_de_Valor
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
            private static string Preparar(string valor)
            {
                UsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(valor), MensagensUsuarios.SENHA_VAZIA);
                return valor.Trim();
            }
        }
    }
