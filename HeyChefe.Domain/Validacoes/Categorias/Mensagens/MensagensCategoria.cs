using Financ.Domain.Validacoes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Validacoes.Categorias.Mensagens
{
    public static class MensagensCategoria
    {
        public static string NOME_OBRIGATORIO => "O nome da categoria é obrigatório.";
        public static string CARACTERES_INVALIDOS => "O nome da categoria deve ter entre 3 e 50 caracteres.";
        public static string USUARIO_NAO_ENCONTRADO => "Usuário não encontrado";
        public static string CONTA_NAO_ENCONTRADA => "Conta não encontrada";
        public static string USUARIO_INATIVO => "Usuário não está ativo!";
        public static string ACESSO_MESTRE_OBRIGATORIO => "Usuário deve possuir acesso mestre para essa implementação.";
        public static string ACESSO_NEGADO => "Usuário deve possuir acesso para essa implementação.";

      
    }
}
