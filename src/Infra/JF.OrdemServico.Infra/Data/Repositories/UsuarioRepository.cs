using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Infra.Data.Common;
using JF.OrdemServico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JF.OrdemServico.Infra.Data.Repositories;

public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(OrdemServicoDbContext context) : base(context) { }

    public async Task<Usuario?> GetByLoginAsync(string login)
    {
        return await _dbSet
            .Include(u => u.ClienteUsuarios)
            .FirstOrDefaultAsync(predicate: u => u.Login.ToLower() == login.ToLower());
    }
}