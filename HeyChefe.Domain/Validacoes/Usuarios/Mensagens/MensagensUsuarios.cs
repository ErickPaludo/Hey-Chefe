using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Usuarios.Mensagens
{
    public static class MensagensUsuarios
    {

        public const string NOME_OBRIGATORIO = "O primeiro nome do usuário é obrigatório.";
        public const string NOME_MINIMO = "O nome não deve possuir menos do que 3 caracteres";
        public const string NOME_MAXIMO = "O nome não deve possuir mais do que 100 caracteres";

        public const string EMAIL_OBRIGATORIO = "O email do usuário é obrigatório.";
        public const string EMAIL_MINIMO = "O email não deve possuir menos do que 6 caracteres";
        public const string EMAIL_MAXIMO = "O email não deve possuir mais do que 256 caracteres";

        public const string MESMA_SENHA = "Senhas identicas.";
        public const string SENHA_VAZIA = "Obrigatório informar uma senha.";

        public const string NOME_INVALIDO = "Nome inválido";
        public const string SEGUNDO_NOME_INVALIDO = "Sobrenome inválido";

        public const string EMAIL_INVALIDO = "Email inválido";

        public const string NOME_NULO = "Nome não pode ser nulo";
        public const string EMAIL_NULO = "Email não pode ser nulo";
        public const string SENHA_NULA = "Senha não pode ser nula";


    }
}
