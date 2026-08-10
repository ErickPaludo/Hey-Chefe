
using HeyChefe.Domain.Entidades.Segurança;
using HeyChefe.Domain.Interfaces.Repositorios.Segurança;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HeyChefe.Infra.Data.Repositorios.Segurança
{
    public class AutenticacoesRepositorio : BaseRepositorio<Autenticacao>, IAutenticacoesRepositorio
    {
        private readonly AppDbContext _contexto;
        public AutenticacoesRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public Task<Autenticacao?> BuscarAuthComUsuarios(Expression<Func<Autenticacao, bool>> predicado)
        {
            return _contexto.Autenticacao.Include(c => c.Usuario).FirstOrDefaultAsync(predicado);
        }
    }
}
