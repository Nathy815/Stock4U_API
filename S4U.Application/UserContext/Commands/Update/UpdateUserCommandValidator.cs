using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(new Guid("00000000-0000-0000-0000-000000000000"))
                    .WithErrorCode("400");

            RuleFor(e => e.Gender)
                .Must(e => e.Equals("Feminino") || e.Equals("Masculino") || e.Equals("Indiferente"))
                    .When(e => !string.IsNullOrEmpty(e.Gender))
                    .WithMessage("Por favor, informe um sexo válido.");

            RuleFor(e => e.Address)
                .MaximumLength(256)
                    .When(e => !string.IsNullOrEmpty(e.Address))
                    .WithMessage("O endereço não pode ser maior que 256 caracteres.");

            RuleFor(e => e.Number)
                .MaximumLength(6)
                    .When(e => !string.IsNullOrEmpty(e.Number))
                    .WithMessage("O número não pode ser maior que 6 caracteres.");

            RuleFor(e => e.Compliment)
                .MaximumLength(30)
                    .When(e => !string.IsNullOrEmpty(e.Compliment))
                    .WithMessage("O endereço não pode ser maior que 30 caracteres.");
        }
    }
}
