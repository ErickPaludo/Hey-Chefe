using System;
using System.Collections.Generic;
using System.Text;

namespace Financ.Domain.Validacoes
{
    public class ExceptionDomain : Exception
    {
        public ExceptionDomain(string? message) : base(message)
        {
        }
    }
}
