using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace HeyChefe.Domain.Interfaces.Repositorios.Pedidos
{
    public class PedidoRepository : BaseRepositorio<Pedido>, IPedidoRepository
    {
        private readonly AppDbContext _contexto;
        public PedidoRepository(AppDbContext contexto) : base(contexto) => _contexto = contexto;

        public async Task<Codigo> UltimoId()
        {
            Pedido? pedido = await _contexto.Pedidos.Include(lp => lp.LinhasPedido)
                .OrderBy(x => x.NumeroPedido.Valor)
                .LastOrDefaultAsync();

            if (pedido is null)
                return Codigo.Create(1);

            int proxNum = pedido.NumeroPedido.Valor + 1;
            return Codigo.Create(proxNum);
        }
    }
}
