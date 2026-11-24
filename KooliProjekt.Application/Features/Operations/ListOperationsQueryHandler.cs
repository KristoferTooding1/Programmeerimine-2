using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Operations
{
    public class ListOperationsQueryHandler : IRequestHandler<ListOperationsQuery, OperationResult<PagedResult<OperationDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListOperationsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<OperationDto>>> Handle(ListOperationsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<OperationDto>>();
            result.Value = await _dbContext.Operations
                .AsNoTracking()
                .OrderByDescending(x => x.Date)
                .Select(x => new OperationDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    Status = x.Status,
                    Cost = x.Cost,
                    CarId = x.CarId,
                    CarRegNum = x.Car.RegNum,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName,
                    OperationTypeId = x.OperationTypeId,
                    OperationTypeName = x.OperationType.Name
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
