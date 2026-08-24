using HeyChefe.Application.DTOs.Mesas.Get;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Mesas.Query;

public record MesaQuery() : IRequest<IEnumerable<MesaDTO>>;