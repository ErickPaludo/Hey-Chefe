using HeyChefe.Application.DTOs.Categoria.Get;
using HeyChefe.Domain.Entidades.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.Mapeamento
{
    public static class CategoriaMapper
    {
        public static CategoriaDTO ParaDTO(Categoria categoria) => new CategoriaDTO(categoria.Codigo.Valor, categoria.Id, categoria.Titulo.Texto, categoria.Cor.Valor);
        public static List<CategoriaDTO> ParaListDTO(IEnumerable<Categoria> categoria) => categoria.Select(ParaDTO).ToList();

    }
}
