using HeyChefe.Domain.Consultas.Pedido;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Pedidos.Query;

public record PedidoQuery() : IRequest<IEnumerable<PedidosDTO>>;