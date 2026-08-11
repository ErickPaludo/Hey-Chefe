using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.DTOs.Pedidos.Post
{
    public record ItemPedidoDTO(Guid Id,int Quantidade,bool Cortesia = false);
}
