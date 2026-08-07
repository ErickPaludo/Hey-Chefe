using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Item;

namespace HeyChefe.Domain.Objetos_de_Valor.Titulo
{
    public sealed record TituloItem : TituloBase
    {
        public static readonly int MaxLenght = 30;
        protected override int TamanhoMaximo => MaxLenght;
        public TituloItem() { }
        private TituloItem(string texto) : base(texto){}
        public static TituloItem Create(string texto) => new(texto);
        protected override void Valida(string texto)
        {
            ItemValidacao.Verifica(texto.Length < TamanhoMinimo || texto.Length > TamanhoMaximo, MensagensBase.TITULO_TAMANHO_INVALIDO(TamanhoMinimo, TamanhoMaximo));
        }
    }
}
