using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Domain.Interfaces.Repositories;

public interface IChamadoRepository : IRepositoryBase<Chamado>
{
    Task<IEnumerable<Chamado>> BuscarPorFiltrosAsync(ChamadoStatus? status, ChamadoPrioridade? prioridade, Guid? clienteId);
}