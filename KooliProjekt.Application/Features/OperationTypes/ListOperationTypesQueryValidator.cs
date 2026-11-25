using FluentValidation;

namespace KooliProjekt.Application.Features.OperationTypes
{
    public class ListOperationTypesQueryValidator : AbstractValidator<ListOperationTypesQuery>
    {
        public ListOperationTypesQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
