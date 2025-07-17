using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.ValueObjects;
using JF.OrdemServico.Infra.Data.Common;
using JF.OrdemServico.Infra.Data.Context;
using MongoDB.Driver;

namespace JF.OrdemServico.Infra.Data.Repositories;

public class ChamadoLogRepository : RepositorioMongoBase<Chamado>, IChamadoRepository
{
    public ChamadoLogRepository(MongoContext context) : base(context, "Chamados")
    {
    }

    public async Task<IEnumerable<Chamado>> BuscarPorFiltrosAsync(ChamadoStatus? status, ChamadoPrioridade? prioridade, Guid? clienteId)
    {
        var builder = Builders<Chamado>.Filter;
        var filtro = builder.Empty;

        if (status != null)
            filtro &= builder.Eq(c => c.Status, status);

        if (prioridade != null)
            filtro &= builder.Eq(c => c.Prioridade, prioridade);

        if (clienteId != null)
            filtro &= builder.Eq(c => c.ClienteId, clienteId);

        var resultado = await _collection.FindAsync(filtro);
        return resultado.ToList();
    }
}