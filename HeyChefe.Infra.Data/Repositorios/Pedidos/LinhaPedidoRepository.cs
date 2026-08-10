using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Interfaces.Repositorios.Base;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;

namespace HeyChefe.Domain.Interfaces.Repositorios.Pedidos
{
    public class LinhaPedidoRepository : BaseRepositorio<LinhaPedido>, ILinhaPedidoRepository
    {
        public LinhaPedidoRepository(AppDbContext contexto) : base(contexto){}
    }
}
