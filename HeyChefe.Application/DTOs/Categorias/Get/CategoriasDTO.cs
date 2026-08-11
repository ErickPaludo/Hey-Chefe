using HeyChefe.Application.DTOs.Categoria.Get;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Categorias.Get
{
    public record CategoriasDTO(List<CategoriaDTO> Categorias);
}
