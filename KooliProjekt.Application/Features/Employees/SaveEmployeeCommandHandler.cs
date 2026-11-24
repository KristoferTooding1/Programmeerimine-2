using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Employees
{
    public class SaveEmployeeCommandHandler : IRequestHandler<SaveEmployeeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveEmployeeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = new Employee();

            if (request.Id == 0)
            {
                _dbContext.Add(entity);
            }
            else
            {
                entity = await _dbContext.FindAsync<Employee>(request.Id);
                if (entity == null)
                {
                    return result.AddError("Cannot find Employee with ID " + request.Id);
                }
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Email = request.Email;
            entity.Role = request.Role;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
