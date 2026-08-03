using Financ.Domain.Enums.Movimentações;
using Financ.Domain.Validacoes;
using Financ.Domain.Validacoes.Movimentações;
using Financ.Domain.Validacoes.Movimentações.Mensagens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Financ.Domain.Objetos_de_Valor
{
    public sealed record Saldo
    {
        public decimal Valor { get; }

        private Saldo(decimal valor)
        {
            ValidaValor(valor);
            Valor = valor;
        }

        public static Saldo Create(decimal valor)
        {
            return new Saldo(valor);
        }

        public Saldo Soma(Saldo saldo)
        {
            ValidaNulo.Verifica(saldo, MensagensMovimentacao.VALOR_NULO);
            return new Saldo(Valor + saldo.Valor);
        }

        public Saldo Subtrai(Saldo saldo)
        {
            ValidaNulo.Verifica(saldo, MensagensMovimentacao.VALOR_NULO);
            return new Saldo(Valor - saldo.Valor);
        }

        private void ValidaValor(decimal valor)
        {
            ValidaNulo.Verifica(valor, MensagensMovimentacao.VALOR_NULO);
        }
    }
}
