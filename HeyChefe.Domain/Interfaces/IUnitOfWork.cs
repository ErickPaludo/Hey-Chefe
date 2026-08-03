using Financ.Domain.Interfaces.Repositorios.Categorias;
using Financ.Domain.Interfaces.Repositorios.ContasBancarias;
using Financ.Domain.Interfaces.Repositorios.Movimentações;
using Financ.Domain.Interfaces.Repositorios.Movimentações.Fixas;
using Financ.Domain.Interfaces.Repositorios.Segurança;
using Financ.Domain.Interfaces.Repositorios.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IContasRepositorio contasRepositorio { get; }
        IContasUsuariosRepositorio contasUsuariosRepositorio { get; }
        IConvitesRepostorio convitesRepostorio { get; }
        IUsuariosRepositorio usuariosRepostorio { get; }
        IAutenticacoesRepositorio autenticacoesRepositorio { get; }
        IMovimentacaoRepositorio movimentacaoRepositorio { get; }
        ICategoriaRepositorio categoriaRepositorio { get; }
        IMovimentacaoCategoriaRepositorio movimentacaoCategoriaRepositorio { get; }
        IMovimentacaoFixaRespositorio movimentacaoFixaRepositorio { get; }
        IMovimentacaoFixaDiariaRespositorio movimentacaoFixaDiariaRepositorio { get; }

        Task Commit();
    }
}
