using JF.OrdemServico.Domain.Common;

namespace JF.OrdemServico.Domain.ValueObjects;

public class ChamadoPrioridade : ValueObjectBase<ChamadoPrioridade>
{
    private ChamadoPrioridade(string value, string name) : base(value, name) { }

    public static readonly ChamadoPrioridade Baixa = new("B", "Baixa");
    public static readonly ChamadoPrioridade Media = new("M", "Média");
    public static readonly ChamadoPrioridade Alta = new("A", "Alta");
    public static readonly ChamadoPrioridade Urgente = new("U", "Urgente");
}