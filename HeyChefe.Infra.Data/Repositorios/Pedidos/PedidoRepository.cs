using HeyChefe.Application.Interfaces.Repository;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Consultas.Pedido;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace HeyChefe.Infra.Data.Repositorios.Pedidos
{
    public class PedidoRepository : BaseRepositorio<Pedido>, IPedidoRepository,IPedidoAppRepository
    {
        private readonly AppDbContext _contexto;
        public PedidoRepository(AppDbContext contexto) : base(contexto) => _contexto = contexto;

        public async Task<IEnumerable<PedidosDTO>> SelecionaPedidos()
        {
            return await _contexto.Pedidos
                .AsNoTracking()
                .Include(x => x.LinhasPedido)
                .ThenInclude(x => x.Item)
                .Include(x => x.Mesa)
                .Include(x => x.Usuario)
                .Select(x => new PedidosDTO(
                        new PedidoDTO(new PedidoCabecalhoDTO(
                                x.Id,
                                x.NumeroPedido.Valor.ToString("d6"),
                                x.Mesa.Id.ToString(),
                                x.Situacao,
                                x.LinhasPedido.Sum(lp => lp.Quantidade),
                                x.ValorFinal(),
                                new CriadorPedidoDTO(x.Usuario.Id, x.Usuario.Nome.Completo),
                                x.LinhasPedido
                                    .Select(lp => new LinhaPedidoDTO(
                                            new ItemLinhaPedidoDTO(
                                                lp.Item.Id,
                                                lp.Item.Nome.Texto,
                                                lp.Item.Descricao != null ?
                                                    lp.Item.Descricao.Texto : null,
                                                lp.Item.PrecoFinal.Valor
                                            ),
                                            lp.Situacao,
                                            lp.Cortesia
                                        )
                                    ).ToList()
                            )
                        )
                    )
                ).ToListAsync();
        }

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