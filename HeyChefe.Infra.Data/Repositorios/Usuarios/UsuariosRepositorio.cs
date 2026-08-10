using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Interfaces.Repositorios.Usuarios;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;

namespace HeyChefe.Infra.Data.Repositorios.Usuarios
{
    public class UsuariosRepositorio : BaseRepositorio<Usuario>, IUsuariosRepositorio
    {
        private AppDbContext _contexto;

        public UsuariosRepositorio(AppDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
