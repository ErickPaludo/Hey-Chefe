using HeyChefe.Application.CQRS.Pedidos.Query;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Teste;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Pedidos.Handler;

public class PedidoHandler : IRequestHandler<PedidoQuery,IEnumerable<SelectPedidos>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PedidoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SelectPedidos>> Handle(PedidoQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<SelectPedidos> pedidos = await _unitOfWork.pedidoRepository.SelectView();
        return pedidos;
    }
}