using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;

namespace HeyChefe.Domain.Objetos_de_Valor.Observação
{
    public sealed record ObservacaoItem : ObservacaoBase
    {
        public ObservacaoItem(string original) : base(original){}
        public ObservacaoItem() { }

        public static ObservacaoItem Create(string texto) => new(texto);
        public override void Valida(string texto)
        {
           ItemValidacao.Verifica(texto.Length > TamanhoMaximo,MensagensBase.OBSERVACAO_TAMANHO_INVALIDO(TamanhoMaximo));
        }
    }
}
