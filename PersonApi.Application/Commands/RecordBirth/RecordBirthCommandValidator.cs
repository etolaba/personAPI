using FluentValidation;

namespace PersonApi.Application.Commands.RecordBirth
{
    public class RecordBirthCommandValidator : AbstractValidator<RecordBirthCommand>
    {
        public RecordBirthCommandValidator()
        {
            RuleFor(x => x.PersonId).GreaterThan(0);

            RuleFor(x => x)
                .Must(x => x.BirthDate.HasValue || !string.IsNullOrWhiteSpace(x.BirthLocation))
                .WithMessage("At least BirthDate or BirthLocation must be provided.");
        }
    }
}
