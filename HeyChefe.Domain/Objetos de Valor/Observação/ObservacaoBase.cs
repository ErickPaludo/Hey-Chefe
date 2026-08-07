
namespace HeyChefe.Domain.Objetos_de_Valor
{
    public abstract record ObservacaoBase
    {
        public static readonly int MaxLenght = 400;
        public virtual int TamanhoMaximo { get; } = MaxLenght;
        public string Texto { get; private set; }

        protected ObservacaoBase() { }
        protected ObservacaoBase(string texto)
        {
            if (!string.IsNullOrEmpty(texto))
            {
                texto = Prepara(texto);
                Valida(texto);
            }

            Texto = texto;
        }

        public abstract void Valida(string texto);
        private string Prepara(string texto)
        {
            return texto.Trim();
        }

    }
}
