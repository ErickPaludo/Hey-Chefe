using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Usuarios;
using Financ.Domain.Validacoes.Usuarios.Mensagens;

namespace Financ.Domain.Objetos_de_Valor
{
    public sealed record Nome
    {
        public string Primeiro { get; }
        public string Segundo { get; }
        public string Completo => $"{Primeiro} {Segundo}";

        private Nome(string primeiroNome, string segundoNome)
        {
            Primeiro = Prepara(primeiroNome);
            Segundo = Prepara(segundoNome);
        }
        public static Nome Create(string primeiroNome, string segundoNome)
        { 
            return new Nome(primeiroNome, segundoNome);
        }
        private static string Prepara(string valor)
        {
            UsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(valor), MensagensUsuarios.NOME_OBRIGATORIO);
            valor = valor.Trim();
            Verifica(valor);
            return valor;
        }
        private static void Verifica(string valor)
        {
            UsuariosValidacao.Verifica(!valor.All(c => char.IsLetter(c) || c == ' '), MensagensUsuarios.NOME_INVALIDO);
            UsuariosValidacao.Verifica(valor.Length > 100, MensagensUsuarios.NOME_MAXIMO);
            UsuariosValidacao.Verifica(valor.Length < 3, MensagensUsuarios.NOME_MINIMO);
        }
    }
}
