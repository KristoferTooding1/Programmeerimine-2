using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Employees
{
    public class ListEmployeesQueryHandler : IRequestHandler<ListEmployeesQuery, OperationResult<PagedResult<EmployeeDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListEmployeesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<EmployeeDto>>> Handle(ListEmployeesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<EmployeeDto>>();
            result.Value = await _dbContext.Employees
                .AsNoTracking()
                .OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Role = x.Role
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
