using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Infra.Data.Context;
using MongoDB.Driver;

namespace JF.OrdemServico.Infra.Data.Common;

public abstract class RepositorioMongoBase<T> : IRepositoryBase<T> where T : EntityBase
{
    protected readonly IMongoCollection<T> _collection;

    protected RepositorioMongoBase(MongoContext context, string nomeColecao)
    {
        _collection = context.GetCollection<T>(nomeColecao);
    }

    public virtual async Task AddAsync(T entidade)
    {
        await _collection.InsertOneAsync(entidade);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var all = await _collection.FindAsync(_ => true);
        return all.ToList();
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        var result = await _collection.FindAsync(filter);
        return await result.FirstOrDefaultAsync();
    }

    public virtual async Task RemoveAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public virtual async Task UpdateAsync(T entidade)
    {
        var filter = Builders<T>.Filter.Eq("Id", entidade.Id);
        await _collection.ReplaceOneAsync(filter, entidade);
    }
}