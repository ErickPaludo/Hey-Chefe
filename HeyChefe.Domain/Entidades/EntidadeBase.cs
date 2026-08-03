using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financ.Domain.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; } = new Guid();
        public DateTime DataHoraRegistro { get;} = DateTime.UtcNow;
        public DateTime? DataHoraAlteracao { get; protected set; }
    }
}
