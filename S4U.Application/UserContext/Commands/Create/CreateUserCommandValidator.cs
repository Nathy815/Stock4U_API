using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Por favor, preencha o nome.")
                .MaximumLength(100)
                    .WithMessage("O nome do usuário não pode ser maior que 100 caracteres.");

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("Por favor, preencha o e-mail.")
                .MaximumLength(200)
                    .WithMessage("O e-mail não pode ser maior que 200 caracteres.")
                .EmailAddress()
                    .WithMessage("Por favor, informe um e-mail válido.");
        }
    }
}
