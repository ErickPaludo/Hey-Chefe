namespace HeyChefe.Domain.Consultas.Pedido;

public record ItemLinhaPedidoDTO(Guid Id, string Nome, string? Descricao, int Quantidade,decimal Valor);