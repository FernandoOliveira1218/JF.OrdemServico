namespace JF.OrdemServico.API.DTOs.Request.Chamados;

public record FiltroChamadoRequest
{
    public string? Status { get; set; }

    public string? Prioridade { get; set; }

    public Guid? ClienteId { get; set; }
}
