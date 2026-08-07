
using HeyChefe.Domain.Validacoes.Codigo;
using HeyChefe.Domain.Validacoes.Codigo.Mensagens;

namespace HeyChefe.Domain.Objetos_de_Valor
{
    public sealed class Codigo
    {
        public int Valor { get; set; }
        public Codigo() { }
        private Codigo(int valor)
        {
            CodigoValidacao.Verifica(valor <= 0, MensagensCodigo.CODIGO_MENOR_IGUAL_ZERO);
            Valor = valor;
        }
        public static Codigo Create(int valor) => new(valor);
    }
}
