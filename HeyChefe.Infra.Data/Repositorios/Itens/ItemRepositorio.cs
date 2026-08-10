using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Interfaces.Repositorios.Base;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Interfaces.Repositorios.Itens
{
    public class ItemRepositorio : BaseRepositorio<Item>, IItemRepositorio
    {
        private readonly AppDbContext _contexto;
        public ItemRepositorio(AppDbContext contexto) : base(contexto) => _contexto = contexto; 

        public async Task<IEnumerable<Item>> RetornarTudo()
        {
            return await _contexto.Items
                .Include(c => c.Categoria)
                .OrderBy(x=> x.Codigo.Valor)
                .ToListAsync();
        }
    }
}
