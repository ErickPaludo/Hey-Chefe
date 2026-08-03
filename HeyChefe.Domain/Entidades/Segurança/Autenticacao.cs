using Financ.Domain.Entidades.Usuarios;
using Financ.Domain.Validacoes.Segurança;
using Financ.Domain.Validacoes.Segurança.Mensagens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades.Segurança
{
    public class Autenticacao
    {
        //public string IdSession { get; private set; }
        //public string IdUsuario { get; private set; }
        //public string? RefreshToken { get; private set; }
        //public long? ExpirationRefresh { get; private set; }
        //public bool Revoke { get; private set; } = false;

        //public Usuario Usuario { get; private set; }

        //public Autenticacao() { }

        //public Autenticacao(string idUsuario, string refreshToken, long expirationRefresh)
        //{
        //    IdSession = Guid.NewGuid().ToString();
        //    IdUsuario = idUsuario;
        //    RefreshToken = refreshToken;
        //    ExpirationRefresh = expirationRefresh;
        //}
        //public void ValidaRefreshToken(string refreshToken)
        //{
        //    AutenticacaoValidacao.Verifica(string.IsNullOrEmpty(refreshToken) || RefreshToken is null || Revoke, MensagensAutenticacao.REFRESH_TOKEN_INVALIDO);
        //}
        //public void AtualizaRefreshToken(string refreshToken, long expirationRefresh)
        //{
        //    RefreshToken = refreshToken;
        //    ExpirationRefresh = expirationRefresh;
        //    Revoke = false;
        //}
        //public void RevokaToken()
        //{
        //    Revoke = true;
        //}

    }
}
