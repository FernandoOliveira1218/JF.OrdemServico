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

    public async Task<IEnumerable<Chamado>> ObterPorStatusAsync(ChamadoStatus status)
    {
        return await _dbSet.Where(c => c.Status.Value == status).ToListAsync();
    }
    
    public async Task<IEnumerable<Chamado>> ObterPorPrioridadeAsync(ChamadoPrioridade prioridade)
    {
        return await _dbSet.Where(c => c.Prioridade.Value == prioridade).ToListAsync();
    }

    public async Task<IEnumerable<Chamado>> ObterPorClienteAsync(Guid clienteId)
    {
        return await _dbSet.Where(c => c.Cliente.Id == clienteId).ToListAsync();
    }
}