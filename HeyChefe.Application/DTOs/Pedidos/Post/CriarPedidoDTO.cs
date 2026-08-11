using HeyChefe.Application.DTOs.Itens.Get;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Pedidos.Post
{
    public record CriarPedidoDTO(Guid MesaId,int Prioridade,List<ItemPedidoDTO> LinhasPedido);
}
