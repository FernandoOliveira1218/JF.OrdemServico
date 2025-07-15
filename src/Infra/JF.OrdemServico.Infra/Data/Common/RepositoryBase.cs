using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JF.OrdemServico.Infra.Data.Common;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
    protected readonly OrdemServicoDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(OrdemServicoDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entidade)
    {
        await _dbSet.AddAsync(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entidade)
    {
        _dbSet.Update(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var entidade = await GetById(id);
        if (entidade is not null)
        {
            _dbSet.Remove(entidade);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}