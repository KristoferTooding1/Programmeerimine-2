using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Operations
{
    public class SaveOperationCommandHandler : IRequestHandler<SaveOperationCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveOperationCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveOperationCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            if (!await _dbContext.Cars.AnyAsync(x => x.Id == request.CarId, cancellationToken))
            {
                result.AddPropertyError("CarId", "Cannot find car with ID " + request.CarId);
            }

            if (!await _dbContext.Employees.AnyAsync(x => x.Id == request.EmployeeId, cancellationToken))
            {
                result.AddPropertyError("EmployeeId", "Cannot find employee with ID " + request.EmployeeId);
            }

            if (!await _dbContext.OperationTypes.AnyAsync(x => x.Id == request.OperationTypeId, cancellationToken))
            {
                result.AddPropertyError("OperationTypeId", "Cannot find operation type with ID " + request.OperationTypeId);
            }

            if (result.HasErrors)
            {
                return result;
            }

            var entity = new Operation();

            if (request.Id == 0)
            {
                _dbContext.Add(entity);
            }
            else
            {
                entity = await _dbContext.FindAsync<Operation>(request.Id);
                if (entity == null)
                {
                    return result.AddError("Cannot find Operation with ID " + request.Id);
                }
            }

            entity.Date = request.Date;
            entity.Status = request.Status;
            entity.Cost = request.Cost;
            entity.CarId = request.CarId;
            entity.EmployeeId = request.EmployeeId;
            entity.OperationTypeId = request.OperationTypeId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
