using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.ValueObjects;
using JF.OrdemServico.Infra.Data.Common;
using JF.OrdemServico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JF.OrdemServico.Infra.Data.Repositories;

public class ChamadoRepository : RepositoryBase<Chamado>, IChamadoRepository
{
    public ChamadoRepository(OrdemServicoDbContext context) : base(context) { }

    public async Task<IEnumerable<Chamado>> BuscarPorFiltrosAsync(ChamadoStatus? status, ChamadoPrioridade? prioridade, Guid? clienteId)
    {
        var query = _dbSet.AsQueryable();

        if (status != null)
            query = query.Where(c => c.Status == status.Value);

        if (prioridade != null)
            query = query.Where(c => c.Prioridade == prioridade.Value);

        if (clienteId.HasValue)
            query = query.Where(c => c.ClienteId == clienteId.Value);

        return await query.ToListAsync();
    }
}