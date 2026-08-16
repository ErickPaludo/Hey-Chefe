using HeyChefe.Domain.Entidades.Pedidos.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Pedidos.Get
{
    public record CabecalhoPedidoDTO(Guid Id,string NumPedido,string Mesa,ESituacaoPedido Situacao,decimal QuantidadeTotalItens,decimal ValorFinal,CriadorPedidoDTO CriadorPedido, List<LinhaPedidoDTO> LinhasPedido);
}
