namespace JF.OrdemServico.API.DTOs.Response.Chamados;

public record ChamadoResponse
{
    public Guid? Id { get; init; }

    public Guid? ClienteId { get; init; }

    public Guid? ResponsavelId { get; init; }

    public string? Titulo { get; init; }

    public string? Descricao { get; init; }

    public string? Status { get; init; }

    public string? ObservacoesFinalizacao { get; init; }

    public DateTime? CriadoEm { get; init; }

    public DateTime? FinalizadoEm { get; init; }


    // Obrigatório para o AutoMapper
    public ChamadoResponse() { }
}