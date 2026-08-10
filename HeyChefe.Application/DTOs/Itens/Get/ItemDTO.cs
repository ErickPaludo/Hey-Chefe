using HeyChefe.Application.DTOs.Categoria.Get;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Itens.Get
{
    public record ItemDTO(Guid id,int Codigo, string Nome, string? Descricao, decimal PrecoCusto, decimal MargemLucro,decimal PrecoVenda, CategoriaDTO? Categoria);
}
