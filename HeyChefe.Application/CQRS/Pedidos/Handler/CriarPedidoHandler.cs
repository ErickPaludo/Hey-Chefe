using HeyChefe.Application.Comun.Enums;
using HeyChefe.Application.CQRS.Pedidos.Command;
using HeyChefe.Application.Services.PermissoesUsuarios;
using HeyChefe.Domain.Entidades.Itens;
using HeyChefe.Domain.Entidades.Mesas;
using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Entidades.Usuarios;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Objetos_de_Valor;
using HeyChefe.Domain.Consultas.Pedido;
using HeyChefe.Domain.Validacoes.Item;
using HeyChefe.Domain.Validacoes.Mesas;
using NetDevPack.SimpleMediator;

namespace HeyChefe.Application.CQRS.Pedidos.Handler
{
    public class CriarPedidoHandler : IRequestHandler<CriarPedidoCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CriarPedidoHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<string> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            Usuario usuario = await _unitOfWork.ValidarUsuario(request.UsuarioId, PermissaoUsuario.CriarPedido);

            //Valida se a mesa Existe
            Mesa? mesa = await _unitOfWork.mesaRepository.BuscarObjetoUnico(x => x.Id == request.MesaId);
            if (mesa is null)
                throw new MesaValidacao("Mesa não encontrada");

            //Busca ultimo Id do pedido
            Codigo codigo = await _unitOfWork.pedidoRepository.UltimoId();

            //Cria Pedido
            Pedido pedido = Pedido.Create(codigo,mesa, usuario);
            //Loop para criar linhas pedidos
            foreach (var linha in request.ItensPedido)
            {
                Item? item = await _unitOfWork.itemRepositorio.BuscarObjetoUnico(x => x.Id == linha.Id);
                LinhaPedido linhaPedido = LinhaPedido.Create(pedido, item!, linha.Quantidade, linha.Cortesia);
                await _unitOfWork.linhaPedidoRepository.Adicionar(linhaPedido);
            }

            await _unitOfWork.pedidoRepository.Adicionar(pedido);
            _unitOfWork.mesaRepository.Atualiza(mesa);
            await _unitOfWork.Commit();

            return $"Pedido n°{codigo.Valor.ToString("D6")} criado";
        }
    }
}
