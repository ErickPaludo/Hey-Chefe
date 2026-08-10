namespace HeyChefe.Domain.Entidades.Base
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime DataHoraRegistro { get; private set; } = DateTime.UtcNow;
        public DateTime? DataHoraAlteracao { get; protected set; }
    }
}
