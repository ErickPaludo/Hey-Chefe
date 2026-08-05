using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Item.Mensagens;

namespace HeyChefe.Domain.Objetos_de_Valor
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
            ValidaNulo.Verifica(saldo, MensagemItem.VALOR_NULO);
            return new Saldo(Valor + saldo.Valor);
        }

        public Saldo Subtrai(Saldo saldo)
        {
            ValidaNulo.Verifica(saldo, MensagemItem.VALOR_NULO);
            return new Saldo(Valor - saldo.Valor);
        }

        public Saldo Porcentagem(Saldo saldo)
        {
            ValidaNulo.Verifica(saldo, MensagemItem.VALOR_NULO);
            return new Saldo(Valor + (saldo.Valor / 100));
        }

        private void ValidaValor(decimal valor)
        {
            ValidaNulo.Verifica(valor, MensagemItem.VALOR_NULO);
        }
    }
}
