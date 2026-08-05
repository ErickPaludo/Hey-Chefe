using HeyChefe.Domain.Entidades.Base;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Objetos_de_Valor.Titulo;
using HeyChefe.Domain.Validacoes;
using HeyChefe.Domain.Validacoes.Base.Mensagens;

namespace HeyChefe.Domain.Entidades.Categorias
{
    public class Categoria : EntidadeBase
    {
        public Codigo Codigo { get; private set; }
        public TituloCategoria Titulo { get; private set; }
        public Cor Cor { get; private set; }
        private Categoria(Codigo codigo, TituloCategoria titulo, Cor cor)
        {
            ValidaNulo.Verifica(codigo,MensagensBase.CODIGO_OBRIGATORIO);
            ValidaNulo.Verifica(titulo,MensagensBase.TITULO_OBRIGATORIO);
            ValidaNulo.Verifica(cor,MensagensBase.COR_OBRIGATORIA);
            Codigo = codigo;
            Titulo = titulo;
            Cor = cor;
        }
        public static Categoria Create(Codigo codigo, TituloCategoria titulo, Cor cor) => new Categoria(codigo,titulo, cor);
        #region Atualiza
        public void AlterarTitulo(TituloCategoria titulo)
        {
            Titulo = titulo;
        }
        public void AlterarCor(Cor cor)
        {
            Cor = cor;
        }
        #endregion
    }
}
