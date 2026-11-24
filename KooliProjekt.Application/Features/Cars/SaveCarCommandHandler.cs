using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class SaveCarCommandHandler : IRequestHandler<SaveCarCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveCarCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveCarCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var entity = new Car();

            if (request.Id == 0)
            {
                _dbContext.Add(entity);
            }
            else
            {
                entity = await _dbContext.FindAsync<Car>(request.Id);
                if (entity == null)
                {
                    return result.AddError("Cannot find Car with ID " + request.Id);
                }
            }

            entity.Make = request.Make;
            entity.Model = request.Model;
            entity.RegNum = request.RegNum;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
