using HeyChefe.Domain.Entidades.Segurança;
using HeyChefe.Domain.Interfaces.Repositorios.Base;
using System.Linq.Expressions;

namespace HeyChefe.Domain.Interfaces.Repositorios.Segurança
{
    public interface IAutenticacoesRepositorio : IBaseRepositorio<Autenticacao>
    {
        Task<Autenticacao?> BuscarAuthComUsuarios(Expression<Func<Autenticacao, bool>> predicado);
    }
}
