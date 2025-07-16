using FluentValidation;
using JF.OrdemServico.Application.Common;
using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.Interfaces.Services;

namespace JF.OrdemServico.Application.Services;

public class ClienteService : ServiceBase<Cliente>, IClienteService
{
    public ClienteService(IClienteRepository repository, IValidator<Cliente> validator) : base(repository, validator)
    {
    }
}