namespace JF.OrdemServico.API.DTOs.Response.Chamados;

public record ChamadoResponse
(
    Guid Id,
    string Titulo,
    string Descricao,
    string Status,
    Guid ClienteId,
    Guid ResponsavelId,
    DateTime CriadoEm,
    DateTime? FinalizadoEm,
    string? ObservacoesFinalizacao
);