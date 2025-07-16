using FluentValidation;
using JF.OrdemServico.Application.Common;
using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.Interfaces.Services;

namespace JF.OrdemServico.Application.Services;

public class ChamadoService : ServiceBase<Chamado>, IChamadoService
{
    public ChamadoService(IChamadoRepository repository, IValidator<Chamado> validator) : base(repository, validator)
    {
    }
}