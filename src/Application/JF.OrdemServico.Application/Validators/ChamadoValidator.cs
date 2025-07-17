using FluentValidation;
using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.Application.Validators;

public class ChamadoValidator : AbstractValidator<Chamado>
{
    public ChamadoValidator()
    {
        RuleFor(x => x.ClienteId)
            .NotNull()
            .WithMessage("ClienteId é obrigatório.");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("Descrição do problema é obrigatória.")
            .MinimumLength(10)
            .WithMessage("Descrição deve ter pelo menos 10 caracteres.");

        RuleFor(x => x.Prioridade)
            .NotNull()
            .WithMessage("Prioridade é obrigatória.");

        RuleFor(x => x.Status)
            .NotNull()
            .WithMessage("Status é obrigatório.");
    }
}