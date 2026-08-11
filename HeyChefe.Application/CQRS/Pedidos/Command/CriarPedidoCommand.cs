using HeyChefe.Application.DTOs.Pedidos.Post;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Application.CQRS.Pedidos.Command
{
    public record CriarPedidoCommand(Guid UsuarioId,Guid MesaId,int Prioridade,List<ItemPedidoDTO> ItensPedido) : IRequest<string>;
}
