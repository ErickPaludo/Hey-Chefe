using HeyChefe.Application.Comun.Resultado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.Comun.Resultado
{
    public class Resultado<T>
    {
        public T? Sucesso { get;private set; }
        public Falha? Falha { get; private set; }
        public bool ValidaSucesso => Falha == null;
        public bool ValidaFalha => !ValidaSucesso;

        private Resultado(T sucesso) { Sucesso = sucesso; }
        private Resultado(Falha falha) { Falha = falha; }
        public static Resultado<T> GeraSucesso(T sucesso) => new(sucesso);
        public static Resultado<T> GeraFalha(Falha falha) => new(falha);
    }
}
