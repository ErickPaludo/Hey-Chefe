using HeyChefe.Domain.Validacoes.Base.Mensagens;
using HeyChefe.Domain.Validacoes.Categorias;

namespace HeyChefe.Domain.Objetos_de_Valor.Titulo
{
    public sealed record TituloCategoria : TituloBase
    {
        public static readonly int MaxLenght = 30;
        protected override int TamanhoMaximo => MaxLenght;
        public TituloCategoria() { }
        private TituloCategoria(string texto) : base(texto){}
        public static TituloCategoria Create(string texto) => new(texto);
        protected override void Valida(string texto)
        {
            CategoriaValidacao.Verifica(texto.Length < TamanhoMinimo || texto.Length > TamanhoMaximo, MensagensBase.TITULO_TAMANHO_INVALIDO(TamanhoMinimo,TamanhoMaximo));
        }
    }
}
