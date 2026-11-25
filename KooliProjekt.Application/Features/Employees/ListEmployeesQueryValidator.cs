using FluentValidation;

namespace KooliProjekt.Application.Features.Employees
{
    public class ListEmployeesQueryValidator : AbstractValidator<ListEmployeesQuery>
    {
        public ListEmployeesQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
