using HeyChefe.Domain.Entidades.Pedidos.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Pedidos.Get
{
    public record PedidoDTO(Guid Id,string NumPedido,int Prioridade,int Mesa,ESituacaoPedido Situacao);
}
