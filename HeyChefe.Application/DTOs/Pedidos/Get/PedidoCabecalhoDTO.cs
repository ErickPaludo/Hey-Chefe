using HeyChefe.Domain.Consultas.Pedido;
using HeyChefe.Domain.Entidades.Pedidos.Enums;

namespace HeyChefe.Domain.Consultas.Pedido
{
    public record PedidoCabecalhoDTO(Guid Id, string NumPedido,int Prioridade, string Mesa, ESituacaoPedido Situacao, decimal QuantidadeTotalItens, decimal ValorFinal, CriadorPedidoDTO CriadorPedidoDto, List<LinhaPedidoDTO> LinhasPedido);
}


