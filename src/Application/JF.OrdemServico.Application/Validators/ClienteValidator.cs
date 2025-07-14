using FluentValidation;
using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.Application.Validators;

public class ClienteValidator : AbstractValidator<Cliente>
{
    public ClienteValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome do cliente é obrigatório.")
            .MinimumLength(3)
            .WithMessage("Nome deve ter pelo menos 3 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("Formato de e-mail inválido.");
    }
}