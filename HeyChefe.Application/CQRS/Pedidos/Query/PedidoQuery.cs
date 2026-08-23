using HeyChefe.Domain.Teste;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Pedidos.Query;

public record PedidoQuery() : IRequest<IEnumerable<SelectPedidos>>;