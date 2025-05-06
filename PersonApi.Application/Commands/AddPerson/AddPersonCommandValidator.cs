using FluentValidation;
using PersonApi.Domain.Enums;
using System;

namespace PersonApi.Application.Commands.AddPerson
{
    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
        {
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);
            RuleFor(x => x.Gender)
            .Must(BeAValidGender)
            .WithMessage("Gender must be one of the following: M, F, Other, Unknown.");

        }
        private bool BeAValidGender(string gender)
        {
            return Enum.TryParse(typeof(Gender), gender, true, out _);
        }

    }
}
