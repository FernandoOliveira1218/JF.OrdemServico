namespace JF.OrdemServico.Domain.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task AddAsync(T entidade);

    Task UpdateAsync(T entidade);

    Task RemoveAsync(Guid id);

    Task<T?> GetById(Guid id);

    Task<IEnumerable<T>> GetAllAsync();
}