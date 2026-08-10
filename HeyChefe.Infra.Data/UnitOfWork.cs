
using HeyChefe.Infra.Data.Repositorios.Usuarios;
using HeyChefe.Domain.Interfaces;
using HeyChefe.Domain.Interfaces.Repositorios.Categorias;
using HeyChefe.Domain.Interfaces.Repositorios.Itens;
using HeyChefe.Domain.Interfaces.Repositorios.Mesas;
using HeyChefe.Domain.Interfaces.Repositorios.Pedidos;
using HeyChefe.Domain.Interfaces.Repositorios.Segurança;
using HeyChefe.Domain.Interfaces.Repositorios.Usuarios;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Categorias;
using HeyChefe.Infra.Data.Repositorios.Segurança;
using Microsoft.EntityFrameworkCore;


namespace HeyChefe.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _contexto;
        private IUsuariosRepositorio _usuarioRepostorio;
        private IAutenticacoesRepositorio _autenticaoRepositorio ;
        private ICategoriaRepositorio _categoriaRepositorio;
        private IItemRepositorio _itemRepositorio;
        private IMesaRepository _mesaRepository;
        private IPedidoRepository _pedidoRepository;
        private ILinhaPedidoRepository _linhaPedidoRepository;
       
        public UnitOfWork(AppDbContext contexto)
        {
            _contexto = contexto;
        }
     
        public IUsuariosRepositorio usuarioRepostorio { get { return _usuarioRepostorio = _usuarioRepostorio ?? new UsuariosRepositorio(_contexto); } }
        public IAutenticacoesRepositorio autenticaoRepositorio { get { return _autenticaoRepositorio = _autenticaoRepositorio ?? new AutenticacoesRepositorio(_contexto); } }
        public ICategoriaRepositorio categoriaRepositorio { get { return _categoriaRepositorio = _categoriaRepositorio ?? new CategoriaRepositorio(_contexto); } }
        public IItemRepositorio itemRepositorio { get { return _itemRepositorio = _itemRepositorio ?? new ItemRepositorio(_contexto); } }
        public IMesaRepository mesaRepository { get { return _mesaRepository = _mesaRepository ?? new MesaRepository(_contexto); } }
        public IPedidoRepository pedidoRepository { get { return _pedidoRepository = _pedidoRepository ?? new PedidoRepository(_contexto); } }
        public ILinhaPedidoRepository linhaPedidoRepository { get { return _linhaPedidoRepository = _linhaPedidoRepository ?? new LinhaPedidoRepository(_contexto); } }

        public async Task Commit()
        {
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Conflito de concorrência. Os dados foram alterados por outro processo.");
            }
        }
    }
}
