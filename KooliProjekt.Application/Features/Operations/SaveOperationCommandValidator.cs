using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Operations
{
    public class SaveOperationCommandValidator : AbstractValidator<SaveOperationCommand>
    {
        public SaveOperationCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).When(x => x.Cost.HasValue)
                .WithMessage("Cost must be greater than or equal to 0.");

            RuleFor(x => x.CarId)
                .GreaterThanOrEqualTo(1).WithMessage("CarId must be greater than or equal to 1.")
                .Custom((carId, context) =>
                {
                    if (!dbContext.Cars.Any(c => c.Id == carId))
                    {
                        context.AddFailure("CarId", "The specified car does not exist.");
                    }
                });

            RuleFor(x => x.EmployeeId)
                .GreaterThanOrEqualTo(1).WithMessage("EmployeeId must be greater than or equal to 1.")
                .Custom((employeeId, context) =>
                {
                    if (!dbContext.Employees.Any(e => e.Id == employeeId))
                    {
                        context.AddFailure("EmployeeId", "The specified employee does not exist.");
                    }
                });

            RuleFor(x => x.OperationTypeId)
                .GreaterThanOrEqualTo(1).WithMessage("OperationTypeId must be greater than or equal to 1.")
                .Custom((operationTypeId, context) =>
                {
                    if (!dbContext.OperationTypes.Any(t => t.Id == operationTypeId))
                    {
                        context.AddFailure("OperationTypeId", "The specified operation type does not exist.");
                    }
                });
        }
    }
}
