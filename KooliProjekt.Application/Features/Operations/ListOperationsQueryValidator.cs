using FluentValidation;

namespace KooliProjekt.Application.Features.Operations
{
    public class ListOperationsQueryValidator : AbstractValidator<ListOperationsQuery>
    {
        public ListOperationsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
