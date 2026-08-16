using HeyChefe.Application.DTOs.Pedidos.Post;
using HeyChefe.Domain.Entidades.Pedidos.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Pedidos.Get
{
    public record LinhaPedidoDTO(ItemLinhaPedido Item, ESituacaoLinhaPedido Situacao, bool Cortesia);
}
