﻿using FluentValidation;
using JF.OrdemServico.Application.Common;
using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Messages;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.Interfaces.Services;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Application.Services;

public class ChamadoService : ServiceBase<Chamado>, IChamadoService
{
    private new readonly IChamadoRepository _repository;
    private readonly IMessageBus _messageBus;
    
    public ChamadoService(IChamadoRepository repository, IMessageBus messageBus, IValidator<Chamado> validator) : base(repository, validator)
    {
        _repository = repository;
        _messageBus = messageBus;
    }

    public async Task<IEnumerable<Chamado>> BuscarComFiltrosAsync(ChamadoStatus? status, ChamadoPrioridade? prioridade, Guid? clienteId)
    {
        return await _repository.BuscarPorFiltrosAsync(status, prioridade, clienteId);
    }

    public async Task<Chamado> FinalizarAsync(Guid id, string? observacoes, DateTime? dataFinalizacao)
    {
        var chamado = await GetByIdAsync(id) ?? throw new KeyNotFoundException("Chamado não encontrado.");

        chamado.AtualizarStatus(ChamadoStatus.Finalizado, observacoes, dataFinalizacao); 

        var validationResult = await _validator.ValidateAsync(chamado);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _repository.UpdateAsync(chamado);

        await _messageBus.PublishAsync("chamado.finalizado", chamado);

        return chamado;
    }
}