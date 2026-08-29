using HeyChefe.Application.CQRS.Pedidos.Query;
using HeyChefe.Application.Interfaces.Repository;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Consultas.Pedido;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Pedidos.Handler;

public class PedidoHandler : IRequestHandler<PedidoQuery,IEnumerable<PedidosDTO>>
{
    private readonly IPedidoAppRepository _pedidosAppRepository;
    

    public PedidoHandler(IPedidoAppRepository pedidosAppRepository)
    {
        _pedidosAppRepository = pedidosAppRepository;
    }

    public async Task<IEnumerable<PedidosDTO>> Handle(PedidoQuery request, CancellationToken cancellationToken)
    {
         IEnumerable<PedidosDTO> pedidos = await _pedidosAppRepository.SelecionaPedidos();
         return pedidos;
    }
}