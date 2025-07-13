using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Domain.Entities;

public class Chamado : EntityBase
{
    public Cliente Cliente { get; private set; }
    public string Descricao { get; private set; }
    public ChamadoPrioridade Prioridade { get; private set; }
    public ChamadoStatus Status { get; private set; }
    public DateTime? DataConclusao { get; private set; }

    public Chamado(Cliente cliente, string descricao, ChamadoPrioridade prioridade)
    {
        Cliente = cliente;
        Descricao = descricao;
        Prioridade = prioridade;
        Status = ChamadoStatus.Aberto;
    }

    public void AtualizarStatus(ChamadoStatus novoStatus)
    {
        Status = novoStatus;

        AtualizarDataAlteracao();

        if (novoStatus == ChamadoStatus.Finalizado)
        {
            DataConclusao = DateTime.UtcNow;
        }
    }
}