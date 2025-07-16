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
        // Obtém o valor do Id da entidade recebida
        var entityIdProperty = typeof(TEntity).GetProperty("Id");
        if (entityIdProperty == null)
            throw new Exception("Entidade não possui propriedade Id.");

        var id = (Guid)entityIdProperty.GetValue(entity)!;

        // Busca a entidade original no banco
        var originalEntity = await _repository.GetById(id);
        if (originalEntity == null)
            throw new KeyNotFoundException($"Entidade com id {id} não encontrada.");

        // Atualiza apenas as propriedades que mudaram
        AtualizarApenasPropriedadesAlteradas(originalEntity, entity);

        // Valida a entidade atualizada
        var validationResult = await _validator.ValidateAsync(originalEntity);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Atualiza no banco (com tracking ativo, EF vai atualizar só as propriedades modificadas)
        await _repository.UpdateAsync(originalEntity);

        return originalEntity;
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

    protected virtual void AtualizarApenasPropriedadesAlteradas(TEntity destino, TEntity origem)
    {
        var props = typeof(TEntity).GetProperties()
                    .Where(p => p.Name != "Id" && p.CanWrite);

        foreach (var prop in props)
        {
            var valorOrigem = prop.GetValue(origem);
            var valorDestino = prop.GetValue(destino);

            bool valoresDiferentes = valorOrigem == null
                ? valorDestino != null
                : !valorOrigem.Equals(valorDestino);

            if (valoresDiferentes)
            {
                prop.SetValue(destino, valorOrigem);
            }
        }
    }
}