using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.Domain.Interfaces.Services;

public interface IChamadoService : IServiceBase<Chamado>
{
    Task<Chamado> FinalizarAsync(Guid id, string? observacoes, DateTime? dataFinalizacao);
}