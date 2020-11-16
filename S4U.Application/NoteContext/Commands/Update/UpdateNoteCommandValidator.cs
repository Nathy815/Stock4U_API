using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.NoteContext.Commands.Update
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                    .WithMessage("Por favor, informe o título da nota.")
                .MaximumLength(100)
                    .WithMessage("O título não pode ser maior que 100 caracteres.");

            RuleFor(e => e.Comments)
                .MaximumLength(256)
                    .When(e => !string.IsNullOrEmpty(e.Comments))
                    .WithMessage("O comentário não pode ser maior que 256 caracteres.");

            RuleFor(e => e.Alert)
                .GreaterThan(DateTime.Now)
                    .When(e => e.Alert.HasValue)
                    .WithMessage("Você não pode escolher uma data de alerta posterior a atual.");
        }
    }
}
