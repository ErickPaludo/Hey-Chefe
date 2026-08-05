using System.Linq.Expressions;

namespace HeyChefe.Domain.Interfaces.Repositorios.Base
{
    public interface IBaseRepositorio<T> where T : class
    {
        Task<T> Adicionar(T entity);
        Task<T?> BuscarPeloId<TId>(TId? id);
        Task<bool> ExisteId(Expression<Func<T, bool>> predicado);
        Task<T?> BuscarObjetoUnico(Expression<Func<T, bool>> predicado);
        Task<IEnumerable<T>> BuscarPorCondicao(Expression<Func<T, bool>> predicado);
        void Atualiza(T entity);
        void Delete(T entity);
    }
}
