using JF.OrdemServico.Domain.Common;

namespace JF.OrdemServico.Domain.ValueObjects;

public class ChamadoStatus : ValueObjectBase
{
    private ChamadoStatus(string value, string name) : base(value, name) { }

    public static readonly ChamadoStatus Aberto = new("A", "Aberto");
    public static readonly ChamadoStatus EmAndamento = new("E", "Em Andamento");
    public static readonly ChamadoStatus Finalizado = new("F", "Finalizado");
    public static readonly ChamadoStatus Cancelado = new("C", "Cancelado");
}