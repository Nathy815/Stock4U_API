using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.PriceContext.Commands.Update
{
    public class UpdatePriceCommandValidator : AbstractValidator<UpdatePriceCommand>
    {
        public UpdatePriceCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                    .WithMessage("Por favor, informe o código do parâmetro.");

            RuleFor(e => e.Price)
                .NotEmpty()
                    .WithMessage("Por favor, informe o valor de alerta.");

            RuleFor(e => e.Type)
                .IsInEnum()
                    .WithMessage("Por favor, informe um parâmetro válido.");
        }
    }
}
