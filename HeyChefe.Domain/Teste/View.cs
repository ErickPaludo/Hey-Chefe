using HeyChefe.Domain.Entidades.Pedidos.Enums;

namespace HeyChefe.Domain.Teste
{
    public record View(Guid Id, string NumPedido, string Mesa, ESituacaoPedido Situacao, decimal QuantidadeTotalItens, decimal ValorFinal, CriadorPedidoDTO CriadorPedido, List<LinhaPedidoDTO> LinhasPedido);
    public record CriadorPedidoDTO(Guid Id, string NomeCompleto);
    public record LinhaPedidoDTO(ItemLinhaPedido Item, ESituacaoLinhaPedido Situacao, bool Cortesia);
    public record ItemLinhaPedido(Guid Id, string Nome, string? Descricao, decimal Valor);
    public record PedidoDTO(Guid Id, string NumPedido, int Prioridade, int Mesa, ESituacaoPedido Situacao);
}


