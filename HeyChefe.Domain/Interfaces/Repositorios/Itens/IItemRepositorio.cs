using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Interfaces.Repositorios.Base;

namespace HeyChefe.Domain.Interfaces.Repositorios.Itens
{
    public interface IItemRepositorio : IBaseRepositorio<Item>
    {
        Task<IEnumerable<Item>> RetornarTudo();
    }
}
