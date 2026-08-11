using HeyChefe.Application.Comun.Enums;
using HeyChefe.Domain.Entidades.Usuarios.Enums;

namespace HeyChefe.Application.Services.PermissoesUsuarios
{
    public static class ServicoPermiteAcesso
    {
        private static readonly Dictionary<EPermissaoUsuario, PermissaoUsuario[]> _permissoes = new()
        {
            [EPermissaoUsuario.Administrador] = 
            [PermissaoUsuario.CriarColaborador,
             PermissaoUsuario.EditarColaborador,
             PermissaoUsuario.ExcluirColaborador,
             PermissaoUsuario.CriarMesa,
             PermissaoUsuario.EditarMesa,
             PermissaoUsuario.ExcluirMesa,
             PermissaoUsuario.CriarCategoria,
             PermissaoUsuario.EditarCategoria,
             PermissaoUsuario.ExcluirCategoria,
             PermissaoUsuario.CriarItem,
             PermissaoUsuario.EditarItem,
             PermissaoUsuario.ExcluirItem,
             PermissaoUsuario.EditarPedido,
             PermissaoUsuario.AlterarSituacaoPedido
            ],

            [EPermissaoUsuario.Garcom] = 
            [PermissaoUsuario.EditarMesa,
             PermissaoUsuario.CriarPedido,
             PermissaoUsuario.EditarPedido,
             PermissaoUsuario.AlterarSituacaoPedido
            ],

            [EPermissaoUsuario.Cozinheiro] = []
        };
        public static bool PossuiPermissao(EPermissaoUsuario acesso, Comun.Enums.PermissaoUsuario permissoes)
            => _permissoes[acesso].Contains(permissoes);
    }
}
