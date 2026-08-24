using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Interfaces.Repositorios.Base;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;

namespace HeyChefe.Infra.Data.Repositorios.Pedidos
{
    public class LinhaPedidoRepository : BaseRepositorio<LinhaPedido>, ILinhaPedidoRepository
    {
        public LinhaPedidoRepository(AppDbContext contexto) : base(contexto){}
    }
}
