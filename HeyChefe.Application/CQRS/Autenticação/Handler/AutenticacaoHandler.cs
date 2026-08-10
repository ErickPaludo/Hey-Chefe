using HeyChefe.Application.Comun.Resultado;
using HeyChefe.Application.CQRS.Autenticação.Commands;
using HeyChefe.Application.DTOs.Autenticação.Get;
using HeyChefe.Application.Interfaces.Autenticação;
using HeyChefe.Application.Interfaces.Segurança;
using HeyChefe.Domain.Entidades.Segurança;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Validacoes.Segurança;
using NetDevPack.SimpleMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeyChefe.Application.CQRS.Autenticação.Handler
{
    public class AutenticacaoHandler : IRequestHandler<AutenticacaoCommand, RetornaTokenDTO>
    {
        private readonly IAutenticacaoServico _autenticacaoServico;
        private readonly ISegurancaServico _segurancaServico;
        private readonly IUnitOfWork _unitOfWork;

        public AutenticacaoHandler(IAutenticacaoServico autenticacaoServico, IUnitOfWork unitOfWork, ISegurancaServico segurancaServico)
        {
            _autenticacaoServico = autenticacaoServico;
            _unitOfWork = unitOfWork;
            _segurancaServico = segurancaServico;
        }

        public async Task<RetornaTokenDTO> Handle(AutenticacaoCommand request, CancellationToken cancellationToken)
        {
           
            request.Email = request.Email.Trim();
            var usuario = await _unitOfWork.usuarioRepostorio.BuscarObjetoUnico(x => x.Email.Endereco == request.Email);

            if (usuario is null)
                throw new AutenticacaoValidacao("Usuário ou senha inválidos!");

            if (!_segurancaServico.ValidaSenhaArgon(usuario.Senha.Hash, request.Senha, usuario.Senha.Salt))
                throw new AutenticacaoValidacao("Usuário ou senha inválidos!");

            var autenticacao = await _unitOfWork.autenticaoRepositorio.BuscarAuthComUsuarios(x => x.Usuario.Id.Equals(usuario.Id));
            var tokenJwt = _autenticacaoServico.GeraToken(usuario.Id.ToString(), request.Email);

            if (autenticacao is null)
            {
                Autenticacao novaAutenticacao = new Autenticacao(usuario, tokenJwt.refreshToken, tokenJwt.expirationRefreshToken);
                await _unitOfWork.autenticaoRepositorio.Adicionar(novaAutenticacao);
            }
            else
            {
                autenticacao.AtualizaRefreshToken(tokenJwt.refreshToken, tokenJwt.expirationRefreshToken);
                _unitOfWork.autenticaoRepositorio.Atualiza(autenticacao);
            }

            await _unitOfWork.Commit();

            return new RetornaTokenDTO
            {
                Expiracao = tokenJwt.expirationTokenFormatado,
                ExpiracaoRefresh = tokenJwt.expirationRefreshTokenFormatado,
                RefreshToken = tokenJwt.refreshToken,
                Token = tokenJwt.token
            };
         
        }
    }
}
