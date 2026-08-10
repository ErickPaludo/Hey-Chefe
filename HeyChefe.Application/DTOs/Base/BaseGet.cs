using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.DTOs.Base
{
    public record BaseGet<T>(T? Conteudo, Meta? Metadados = null) where T : class;
}
