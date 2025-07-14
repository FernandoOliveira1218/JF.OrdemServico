using FluentValidation;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.Interfaces.Services;

namespace JF.OrdemServico.Application.Common;

public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
{
    protected readonly IRepositoryBase<TEntity> _repository;
    protected readonly IValidator<TEntity> _validator;

    protected ServiceBase(IRepositoryBase<TEntity> repository, IValidator<TEntity> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
            
        await _repository.AddAsync(entity);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
            
        await _repository.UpdateAsync(entity);
        return entity;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _repository.GetById(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public virtual async Task<bool> RemoveAsync(Guid id)
    {
        await _repository.RemoveAsync(id);
        return true;
    }
}