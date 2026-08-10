using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.DTOs.Categoria.Get
{
    public record CategoriaDTO(Guid IdCategoria,string nome, string cor);
}
