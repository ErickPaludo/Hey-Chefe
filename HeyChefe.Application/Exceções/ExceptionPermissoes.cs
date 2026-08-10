using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.Exceções
{
    public class ExceptionPermissoes : Exception
    {
        public ExceptionPermissoes(string? message) : base(message)
        {
        }
    }
}
