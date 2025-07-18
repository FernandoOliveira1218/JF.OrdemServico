using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Infra.Data.Common;
using System.Text.Json.Serialization;

namespace JF.OrdemServico.Worker.DTOs;

public class ChamadoLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Descricao { get; set; } = null!;

    public string? Observacao { get; set; }

    [JsonConverter(typeof(ComplexToStringConverter))]
    public string? Prioridade { get; set; } = null!;  // string, pois você vai salvar "M" por exemplo

    [JsonConverter(typeof(ComplexToStringConverter))]
    public string? Status { get; set; } = null!;

    public DateTime? DataConclusao { get; set; }

    public Guid ClienteId { get; set; }
}