using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.OperationTypes
{
    public class SaveOperationTypeCommandHandler : IRequestHandler<SaveOperationTypeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveOperationTypeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveOperationTypeCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = new OperationType();

            if (request.Id == 0)
            {
                _dbContext.Add(entity);
            }
            else
            {
                entity = await _dbContext.FindAsync<OperationType>(request.Id);
                if (entity == null)
                {
                    return result.AddError("Cannot find OperationType with ID " + request.Id);
                }
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
