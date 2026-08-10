using HeyChefe.Application.DTOs.Itens.Get;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Itens.Query
{
    public record ItensQuey() : IRequest<ItensDTO>;
}
