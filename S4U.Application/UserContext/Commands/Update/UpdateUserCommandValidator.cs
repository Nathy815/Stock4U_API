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

            RuleFor(e => e.ZipCode)
                .MaximumLength(8)
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("O CEP não pode ser maior que 8 caracteres.");

            RuleFor(e => e.Local)
                .NotEmpty()
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("Por favor, preencha todos os campos de endereço.")
                .MaximumLength(200)
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("O logradouro não pode ser maior que 200 caracteres.");

            RuleFor(e => e.Number)
                .MaximumLength(20)
                    .When(e => !string.IsNullOrEmpty(e.Number))
                    .WithMessage("O número não pode ser maior que 20 caracteres.");

            RuleFor(e => e.Compliment)
                .MaximumLength(150)
                    .When(e => !string.IsNullOrEmpty(e.Compliment))
                    .WithMessage("O complemento não pode ser maior que 150 caracteres.");

            RuleFor(e => e.Neighborhood)
                .NotEmpty()
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("Por favor, preencha todos os campos de endereço.")
                .MaximumLength(100)
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("O bairro não pode ser maior que 100 caracteres.");

            RuleFor(e => e.City)
                .NotEmpty()
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("Por favor, preencha todos os campos de endereço.")
                .MaximumLength(100)
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("A cidade não pode ser maior que 100 caracteres.");

            RuleFor(e => e.State)
                .NotEmpty()
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("Por favor, preencha todos os campos de endereço.")
                .MaximumLength(2)
                    .When(e => !string.IsNullOrEmpty(e.ZipCode))
                    .WithMessage("O Estado não pode ser maior que 2 caracteres.");
        }
    }
}
