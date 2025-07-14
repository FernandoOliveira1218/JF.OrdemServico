namespace JF.OrdemServico.Domain.Interfaces.Services;

public interface IServiceBase<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<bool> RemoveAsync(Guid id);
}