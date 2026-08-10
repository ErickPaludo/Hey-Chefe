using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;

namespace HeyChefe.Domain.Interfaces.Repositorios.Pedidos
{
    public class PedidoRepository : BaseRepositorio<Pedido>, IPedidoRepository
    {
        public PedidoRepository(AppDbContext contexto) : base(contexto){}
    }
}
