namespace JF.OrdemServico.API.DTOs.Request.Chamados;

public record FinalizarChamadoRequest
(
    string Observacoes,
    DateTime DataFinalizacao
);