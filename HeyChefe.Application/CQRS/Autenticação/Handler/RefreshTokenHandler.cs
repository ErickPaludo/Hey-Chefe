using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Autenticação.Commands;
using HeyChefe.Application.DTOs.Autenticação.Get;
using HeyChefe.Application.Interfaces.Autenticação;
using HeyChefe.Domain.Entidades.Segurança;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Validacoes.Segurança;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Autenticação.Handler
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RetornaTokenDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAutenticacaoServico _tokenService;
        public RefreshTokenHandler(IUnitOfWork unitOfWork, IAutenticacaoServico tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<RetornaTokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
               if(request.refreshToken is null)
                throw new AutenticacaoValidacao("Token expirado.");

                Autenticacao? auth = await _unitOfWork.autenticaoRepositorio.BuscarAuthComUsuarios(x => x.RefreshToken!.Equals(request.refreshToken));

                if (auth is null)
                    throw new AutenticacaoValidacao("Token expirado.");

                auth.ValidaRefreshToken(request.refreshToken);

                var refreshToken = _tokenService.RefreshToken(auth!, request.refreshToken);

                auth.AtualizaRefreshToken(refreshToken.refreshToken, refreshToken.expirationRefreshToken);
                _unitOfWork.autenticaoRepositorio.Atualiza(auth);
                await _unitOfWork.Commit();
                return new RetornaTokenDTO
                {
                    Expiracao = refreshToken.expirationTokenFormatado,
                    ExpiracaoRefresh = refreshToken.expirationRefreshTokenFormatado,
                    RefreshToken = refreshToken.refreshToken,
                    Token = refreshToken.token,
                };
           
        }
    }
}
