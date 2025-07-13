using Microsoft.EntityFrameworkCore;

namespace JF.OrdemServico.Infra.Data.Context;

public class OrdemServicoDbContext : DbContext
{
    public OrdemServicoDbContext(DbContextOptions<OrdemServicoDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdemServicoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}