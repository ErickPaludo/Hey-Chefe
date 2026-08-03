using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Base.Mensagens
{
    public static class MensagensBase
    {
        public const string ID_IGUAL_MENOR_ZERO = "Id não pode ser menor que zero";
        public const string DATA_REGISTRO_INVALIDA = "Deve ser registrada a data atual, esta não pode ser manipulada.";
        public const string USUARIO_NAO_INFORMADO = "Usuário não informado!";
        public const string STATUS_INVALIDO = "Status inválido.";
        public const string LIMITE_USUARIOS_MESTRES = "O limite de usuários mestres foi atingido. ";
        public const string USUARIO_INATIVO_NAO_PODE_SER_ATUALIZADO = "O usuário não está ativo!";
        public const string LIMITE_DE_CONVITES_PARA_USUARIOS_MESTRE = "Numero máximo de convites para usuários mestres atingido.";
        public const string TEMPO_NULO = "O tempo de expiração não pode ser nulo.";

        public static string TITULO_NULO = "Título não pode ser nulo.";
        public static string CONTA_NULA = "Conta não pode ser nula.";
        public static string USUARIO_NULO = "Usuário não pode ser nulo.";
        public static string CONVITE_NULO = "Convite não pode ser nulo.";
        public static string SALDO_NULO = "Saldo não pode ser nulo.";

        public static string TITULO_TAMANHO_INVALIDO(int min, int max) => $"O título deve possuir entre {min} e {max} caracteres.";

        public static string OBSERVACAO_TAMANHO_INVALIDO(int tamanhoMaximo) => $"A observação deve possuir no máximo {tamanhoMaximo} caracteres.";
        

        public const string REMETENTE_NULO = "O remetente do convite não pode ser nulo.";
        public const string DESTINATARIO_NULO = "O destinatário do convite não pode ser nulo.";
        public const string ACESSO_INVALIDO = "Acesso inválido.";
    }
}
