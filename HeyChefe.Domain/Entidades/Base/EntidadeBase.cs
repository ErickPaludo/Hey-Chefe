namespace HeyChefe.Domain.Entidades.Base
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; } = new Guid();
        public DateTime DataHoraRegistro { get;} = DateTime.UtcNow;
        public DateTime? DataHoraAlteracao { get; protected set; }
    }
}
