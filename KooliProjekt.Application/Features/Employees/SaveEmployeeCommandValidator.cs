using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Employees
{
    public class SaveEmployeeCommandValidator : AbstractValidator<SaveEmployeeCommand>
    {
        public SaveEmployeeCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .MaximumLength(50).WithMessage("Role must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
                .Custom((email, context) =>
                {
                    var command = context.InstanceToValidate;
                    var existingEmployee = dbContext
                        .Employees
                        .Where(e => e.Email == command.Email &&
                                    e.Id != command.Id)
                        .FirstOrDefault();

                    if (existingEmployee != null)
                    {
                        context.AddFailure("Email", "An Employee with the same email already exists.");
                    }
                });
        }
    }
}
