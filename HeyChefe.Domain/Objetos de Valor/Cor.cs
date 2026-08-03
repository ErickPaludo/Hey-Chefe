using Financ.Domain.Validacoes.Cor;
using Financ.Domain.Validacoes.Cor.Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Financ.Domain.Objetos_de_Valor
{
    public sealed record Cor
    {
        public string Valor { get; private set; }
        private Cor() { }

        private Cor(string valor)
        {
            ValidaCor(valor);
            Valor = valor;
        }

        public static Cor Create(string valor) => new(valor);

        private void ValidaCor(string? valor)
        {
            if (valor is not null)
            {
                CorValidacao.Verifica(string.IsNullOrEmpty(valor), MensagemCor.COR_INVALIDA);
                CorValidacao.Verifica(!Regex.IsMatch(valor, "^#([0-9A-Fa-f]{6})$"), MensagemCor.COR_INVALIDA);
            }
        }
    }
}
