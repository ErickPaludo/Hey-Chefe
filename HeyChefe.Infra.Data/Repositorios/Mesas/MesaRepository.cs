using HeyChefe.Domain.Entidades.Mesas;
using HeyChefe.Domain.Interfaces.Repositorios.Base;
using HeyChefe.Infra.Data.Contexto;
using HeyChefe.Infra.Data.Repositorios.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeyChefe.Domain.Interfaces.Repositorios.Mesas
{
    public class MesaRepository : BaseRepositorio<Mesa>, IMesaRepository
    {
        public MesaRepository(AppDbContext contexto) : base(contexto){}
    }
}
