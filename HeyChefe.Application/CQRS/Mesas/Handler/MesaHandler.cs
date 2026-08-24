using HeyChefe.Application.CQRS.Mesas.Query;
using HeyChefe.Application.DTOs.Mesas.Get;
using HeyChefe.Application.Mapeamento;
using HeyChefe.Domain.Entidades.Mesas;
using HeyChefe.Domain.Interfaces;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Mesas.Handler;

public class MesaHandler : IRequestHandler<MesaQuery,IEnumerable<MesaDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public MesaHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MesaDTO>> Handle(MesaQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Mesa> mesas = await _unitOfWork.mesaRepository.BuscarPorCondicao(x => 1 == 1);
        return MesaMapper.ParaDTO(mesas);
    }
}