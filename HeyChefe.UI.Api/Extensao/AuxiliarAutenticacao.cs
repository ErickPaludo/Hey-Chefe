using System.Security.Claims;

namespace HeyChefe.UI.Api.Extensao
{
    public static class AuxiliarAutenticacao
    {
        public static Guid RetornaIdUsuario(this ClaimsPrincipal usuario)
        {
            return Guid.Parse(usuario.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
