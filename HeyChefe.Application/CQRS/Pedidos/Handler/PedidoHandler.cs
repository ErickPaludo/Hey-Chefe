using HeyChefe.Application.CQRS.Pedidos.Query;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Consultas.Pedido;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Pedidos.Handler;

public class PedidoHandler : IRequestHandler<PedidoQuery,IEnumerable<PedidosView>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PedidoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PedidosView>> Handle(PedidoQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PedidosView> pedidos = await _unitOfWork.pedidoRepository.SelectView();
        return pedidos;
    }
}