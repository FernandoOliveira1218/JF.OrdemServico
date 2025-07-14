using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Infra.Data.Common;
using JF.OrdemServico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JF.OrdemServico.Infra.Data.Repositories;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(OrdemServicoDbContext context) : base(context) { }

    public async Task<Cliente?> ObterPorEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
    }
}