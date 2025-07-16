using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Domain.Entities;

public class Chamado : EntityBase
{
    public Cliente Cliente { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public string Descricao { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public ChamadoPrioridade Prioridade { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public ChamadoStatus Status { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public DateTime? DataConclusao { get; private set; }

    public Chamado()
    {
        // Construtor usado pelo EF Core
    }

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