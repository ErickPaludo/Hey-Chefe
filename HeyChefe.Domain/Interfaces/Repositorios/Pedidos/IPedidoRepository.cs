using HeyChefe.Domain.Entidades.Pedidos;
using HeyChefe.Domain.Interfaces.Repositorios.Base;
using HeyChefe.Domain.Objetos_de_Valor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Infra.Data.Repositorios.Pedidos
{
    public interface IPedidoRepository : IBaseRepositorio<Pedido>
    {
        Task<Codigo> UltimoId();
    }
}
