using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.DTOs.Base
{
    public record BasePost<T>(T Valor) where T : class;

}
