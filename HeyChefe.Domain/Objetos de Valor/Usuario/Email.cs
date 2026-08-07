using HeyChefe.Domain.Validacoes.Usuarios;
using HeyChefe.Domain.Validacoes.Usuarios.Mensagens;
using System.Net.Mail;

namespace HeyChefe.Domain.Objetos_de_Valor
{
    public sealed record Email
    {
        public string Endereco { get; }
        public static readonly int MaxEndereco = 256;
        public static readonly int MinEndereco = 6;
        private Email(string endereco)
        {
            endereco = Prepara(endereco);
            Endereco = endereco;
        }
        public Email() { }
        public static Email Create(string endereco)
        {
            return new Email(endereco);
        }
        private static string Prepara(string email)
        {
            UsuariosValidacao.Verifica(string.IsNullOrWhiteSpace(email), MensagensUsuarios.EMAIL_OBRIGATORIO);

            email = email.Trim();
            Valida(email);
            return email;
        }

        private static void Valida(string email)
        {
            UsuariosValidacao.Verifica(email.Contains(" "), MensagensUsuarios.EMAIL_INVALIDO);
            UsuariosValidacao.Verifica(email.Length < MinEndereco, MensagensUsuarios.EMAIL_MINIMO);
            UsuariosValidacao.Verifica(email.Length > MaxEndereco, MensagensUsuarios.EMAIL_MAXIMO);
            UsuariosValidacao.Verifica(!ValidaFormato(email), MensagensUsuarios.EMAIL_INVALIDO);
        }

        private static bool ValidaFormato(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
