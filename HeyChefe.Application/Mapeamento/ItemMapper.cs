using HeyChefe.Application.DTOs.Itens.Get;
using HeyChefe.Domain.Entidades.Itens;


namespace HeyChefe.Application.Mapeamento
{
    public static class ItemMapper
    {
        public static ItensDTO ParaDTO(IEnumerable<Item> itens)
        {
            List<ItemDTO> itensDTO = new();
            foreach (var item in itens)
            {
                itensDTO.Add(new ItemDTO(
                    item.Id,
                    item.Codigo.Valor,
                    item.Nome.Texto,
                    item.Descricao == null ? null : item.Descricao.Texto,
                    item.PrecoCusto.Valor,
                    item.MargemLucro.Valor,
                    item.PrecoFinal.Valor,
                    item.Categoria == null ? null : CategoriaMapper.ParaDTO(item.Categoria)
                    )
                );
            }

            return new ItensDTO(itensDTO.OrderBy(x => x.Codigo).ToList());
        }

    }
}
