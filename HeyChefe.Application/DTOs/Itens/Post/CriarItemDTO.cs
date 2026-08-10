using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Itens.Post
{
    public record CriarItemDTO(int Codigo,string Nome,string Descricao,Decimal PrecoCusto,Decimal MargemLucro,Guid? CategoriaId);
}
