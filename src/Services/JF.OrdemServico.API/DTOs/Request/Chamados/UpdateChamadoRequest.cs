namespace JF.OrdemServico.API.DTOs.Request.Chamados;

public record UpdateChamadoRequest
(
    Guid Id,
    string Titulo,
    string Descricao,
    Guid ResponsavelId
);