using System.Diagnostics.CodeAnalysis;

namespace HeyChefe.Domain.Validacoes
{
    public static class ValidaNulo
    {
        public static void Verifica([NotNull]object? objeto, string mensagem)
        {
            if (objeto == null)
                throw new ExceptionDomain(mensagem);
        }
    }
}
