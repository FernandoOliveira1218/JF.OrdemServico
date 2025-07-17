using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Domain.Entities;

public class Chamado : EntityBase
{
    public string Descricao { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    public string? Observacao { get; private set; }

    public ChamadoPrioridade Prioridade { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    public ChamadoStatus Status { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    public DateTime? DataConclusao { get; private set; }

    #region ForeignKey EF

    public Guid ClienteId { get; private set; }

    #endregion

    #region Navigation EF

    public Cliente Cliente { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    #endregion

    public Chamado()
    {
        // Construtor usado pelo EF Core
        Status = ChamadoStatus.Aberto;
        Prioridade = ChamadoPrioridade.Media; // Definindo uma prioridade padrão
    }

    public Chamado(Guid clienteId, string descricao, ChamadoPrioridade prioridade, string? observacao) : base()
    {
        ClienteId = clienteId;
        Descricao = descricao;
        Prioridade = prioridade;
        Observacao = observacao;
    }

    public void AtualizarStatus(ChamadoStatus novoStatus, string? observacao, DateTime? dataConclusao)
    {
        Status = novoStatus;
        Observacao = observacao;

        AtualizarDataAlteracao();

        if (novoStatus == ChamadoStatus.Finalizado)
        {
            DataConclusao = dataConclusao ?? DateTime.UtcNow;
        }
    }
}