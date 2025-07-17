using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Domain.Interfaces.Services;

public interface IChamadoService : IServiceBase<Chamado>
{
    Task<IEnumerable<Chamado>> BuscarComFiltrosAsync(ChamadoStatus? status, ChamadoPrioridade? prioridade, Guid? clienteId);

    Task<Chamado> FinalizarAsync(Guid id, string? observacoes, DateTime? dataFinalizacao);
}