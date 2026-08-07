using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;

namespace HeyChefe.Domain.Objetos_de_Valor
{
    public sealed record NomeUsuario
    {
        public string Primeiro { get; }
        public string Segundo { get; }
        public string Completo => $"{Primeiro} {Segundo}";

        public static readonly int MaxPrimeiro = 50;
        public static readonly int MinPrimeiro = 3;
        public static readonly int MaxSegundo = 50;
        public static readonly int MinSegundo = 3;
        public NomeUsuario() { }
        private NomeUsuario(string primeiroNome, string segundoNome)
        {
            VerificaPrimeiro(primeiroNome);
            VerificaSegundo(segundoNome);
            primeiroNome = Prepara(primeiroNome);
            segundoNome = Prepara(segundoNome);
            Primeiro = Prepara(primeiroNome);
            Segundo = Prepara(segundoNome);
        }
        public static NomeUsuario Create(string primeiroNome, string segundoNome)
        { 
            return new NomeUsuario(primeiroNome, segundoNome);
        }
        private static string Prepara(string valor)
        {
            UsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(valor), MensagensUsuarios.NOME_OBRIGATORIO);
            valor = valor.Trim();
            return valor;
        }
        private static void VerificaPrimeiro(string valor)
        {
            ValidaNulo.Verifica(valor, MensagensUsuarios.NOME_NULO);
            UsuariosValidacao.Verifica(!valor.All(c => char.IsLetter(c) || c == ' '), MensagensUsuarios.NOME_INVALIDO);
            UsuariosValidacao.Verifica(valor.Length > MaxPrimeiro, MensagensUsuarios.NOME_MAXIMO);
            UsuariosValidacao.Verifica(valor.Length < MinPrimeiro, MensagensUsuarios.NOME_MINIMO);
        }
        private static void VerificaSegundo(string valor)
        {
            UsuariosValidacao.Verifica(!valor.All(c => char.IsLetter(c) || c == ' '), MensagensUsuarios.NOME_INVALIDO);
            UsuariosValidacao.Verifica(valor.Length > MaxSegundo, MensagensUsuarios.NOME_MAXIMO);
            UsuariosValidacao.Verifica(valor.Length < MinSegundo, MensagensUsuarios.NOME_MINIMO);
        }
    }
}
