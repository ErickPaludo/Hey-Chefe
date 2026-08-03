using Financ.Domain.Entidades.Segurança;
using Financ.Domain.Interfaces.Repositorios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Interfaces.Repositorios.Segurança
{
    public interface IAutenticacoesRepositorio : IBaseRepositorio<Autenticacao>
    {
        Task<Autenticacao?> BuscarAuthComUsuarios(Expression<Func<Autenticacao, bool>> predicado);
    }
}
