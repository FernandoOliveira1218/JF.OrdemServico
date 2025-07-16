namespace JF.OrdemServico.API.DTOs.Request.Chamados;

public record CreateChamadoRequest
(
    string Titulo,
    string Descricao,
    Guid ClienteId,
    Guid ResponsavelId
);