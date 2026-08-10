using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.DTOs.Base
{
    public class Meta
    {
        public int total { get; set; }
        public int pagina { get; set; }
        public int tamanho { get; set; }
        public int totalPaginas { get; set; }
        public object? filtros { get; set; }
    }
}
