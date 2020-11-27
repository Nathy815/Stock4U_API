using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.PriceContext.Commands.Create
{
    public class CreatePriceCommandValidator : AbstractValidator<CreatePriceCommand>
    {
        public CreatePriceCommandValidator()
        {
            RuleFor(e => e.UserID)
                .NotEmpty()
                    .WithMessage("Por favor, informe o código do usuário.");

            RuleFor(e => e.EquityID)
                .NotEmpty()
                    .WithMessage("Por favor, informa o código da ação.");

            RuleFor(e => e.Price)
                .NotEmpty()
                    .WithMessage("Por favor, informe o valor de alerta.");

            RuleFor(e => e.Type)
                .IsInEnum()
                    .WithMessage("Por favor, informe um parâmetro válido.");
        }
    }
}
