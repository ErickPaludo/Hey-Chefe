using HeyChefe.Domain.Entidades.Categorias;
using HeyChefe.Domain.Interfaces.Repositorios.Categorias;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HeyChefe.Infra.Data.Repositorios.Categorias
{
    public class CategoriaRepositorio : BaseRepositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly AppDbContext _contexto;
        public CategoriaRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Categoria>> RetornarTudo()
        {
            return await _contexto.Categorias
                .OrderBy(x => x.Titulo.Texto)
                .ToListAsync();
        }
    }
}
