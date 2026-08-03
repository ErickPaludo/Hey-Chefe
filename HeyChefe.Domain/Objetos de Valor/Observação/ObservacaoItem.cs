using Financ.Domain.Validacoes.Base.Mensagens;
using Financ.Domain.Validacoes.Movimentações;
using System;
using System.Collections.Generic;
using System.Text;

namespace Financ.Domain.Objetos_de_Valor.Observação
{
    public sealed record ObservacaoMovimentacao : ObservacaoBase
    {
        public ObservacaoMovimentacao(string original) : base(original){}

        public static ObservacaoMovimentacao Create(string texto) => new(texto);
        public override void Valida(string texto)
        {
            MovimentacaoValidacao.Verifica(texto.Length > TamanhoMaximo,MensagensBase.OBSERVACAO_TAMANHO_INVALIDO(TamanhoMaximo));
        }
    }
}
