using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Commands.Compare
{
    public class CompareEquityCommandValidator : AbstractValidator<CompareEquityCommand>
    {
        public CompareEquityCommandValidator()
        {
            RuleFor(e => e.Ticker)
                .NotEmpty()
                    .WithMessage("Por favor, informe o ticker da ação.")
                .MaximumLength(10)
                    .WithMessage("O ticker da ação não pode ser maior que 10 caracteres.");

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Por favor, informe o nome da ação.")
                .MaximumLength(150)
                    .WithMessage("O nome da ação não pode ser maior que 150 caracteres.");

            RuleFor(e => e.EquityID)
                .NotEqual(new Guid("00000000-0000-0000-0000-000000000000"))
                    .WithErrorCode("400");

            RuleFor(e => e.UserID)
                .NotEqual(new Guid("00000000-0000-0000-0000-000000000000"))
                    .WithErrorCode("400");
        }
    }
}