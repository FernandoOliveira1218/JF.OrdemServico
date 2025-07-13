namespace JF.OrdemServico.Domain.Common;
public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime? DataAlteracao { get; protected set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarDataAlteracao()
    {
        DataAlteracao = DateTime.UtcNow;
    }
}
