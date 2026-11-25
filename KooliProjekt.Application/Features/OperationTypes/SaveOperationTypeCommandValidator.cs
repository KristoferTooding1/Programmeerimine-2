using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.OperationTypes
{
    public class SaveOperationTypeCommandValidator : AbstractValidator<SaveOperationTypeCommand>
    {
        public SaveOperationTypeCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
                .Custom((name, context) =>
                {
                    var command = context.InstanceToValidate;
                    var existingType = dbContext
                        .OperationTypes
                        .Where(t => t.Name == command.Name &&
                                    t.Id != command.Id)
                        .FirstOrDefault();

                    if (existingType != null)
                    {
                        context.AddFailure("Name", "An OperationType with the same name already exists.");
                    }
                });

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        }
    }
}
