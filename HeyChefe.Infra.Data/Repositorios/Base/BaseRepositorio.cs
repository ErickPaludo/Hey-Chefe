using HeyChefe.Domain.Interfaces.Repositorios.Base;
using HeyChefe.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HeyChefe.Infra.Data.Repositorios.Base
{
    public class BaseRepositorio<T> : IBaseRepositorio<T> where T : class
    {
        private readonly AppDbContext _contexto;
        public BaseRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<T> Adicionar(T entity)
        {
            await _contexto.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T?> BuscarPeloId<TId>(TId? id)
        {
            return await _contexto.Set<T>().FindAsync(id);
        }

        public async Task<bool> ExisteId(Expression<Func<T,bool>> predicado)
        {
            return await _contexto.Set<T>().AnyAsync(predicado);
        }
        public async Task<T?> BuscarObjetoUnico(Expression<Func<T, bool>> predicado)
        {
            return await _contexto.Set<T>().FirstOrDefaultAsync(predicado);
        }
        public async Task<IEnumerable<T>> BuscarPorCondicao(Expression<Func<T, bool>> predicado)
        {
            return await _contexto.Set<T>().Where(predicado).ToListAsync();
        }
        public void Atualiza(T entity)
        {
          _contexto.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _contexto.Set<T>().Remove(entity);
        }
    }
}
