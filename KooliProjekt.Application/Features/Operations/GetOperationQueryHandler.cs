using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Operations
{
    public class GetOperationQueryHandler : IRequestHandler<GetOperationQuery, OperationResult<OperationDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetOperationQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<OperationDto>> Handle(GetOperationQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<OperationDto>();
            result.Value = await _dbContext.Operations
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
