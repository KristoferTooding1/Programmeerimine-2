using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Cars
{
    public class SaveCarCommandValidator : AbstractValidator<SaveCarCommand>
    {
        public SaveCarCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Make)
                .NotEmpty().WithMessage("Make is required.")
                .MaximumLength(50).WithMessage("Make must not exceed 50 characters.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");

            RuleFor(x => x.RegNum)
                .NotEmpty().WithMessage("RegNum is required.")
                .MaximumLength(10).WithMessage("RegNum must not exceed 10 characters.")
                .Custom((regNum, context) =>
                {
                    var command = context.InstanceToValidate;
                    var existingCar = dbContext
                        .Cars
                        .Where(c => c.RegNum == command.RegNum &&
                                    c.Id != command.Id)
                        .FirstOrDefault();

                    if (existingCar != null)
                    {
                        context.AddFailure("RegNum", "A Car with the same registration number already exists.");
                    }
                });
        }
    }
}
