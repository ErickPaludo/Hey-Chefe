using Financ.Domain.Validacoes.Usuarios;
using Financ.Domain.Validacoes.Usuarios.Mensagens;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Financ.Domain.Objetos_de_Valor
{
    public sealed record Email
    {
        public string Endereco { get; }
        private Email(string endereco)
        {
            endereco = Prepara(endereco);
            Endereco = endereco;
        }
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
            UsuariosValidacao.Verifica(email.Length < 6, MensagensUsuarios.EMAIL_MINIMO);
            UsuariosValidacao.Verifica(email.Length > 256, MensagensUsuarios.EMAIL_MAXIMO);
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
