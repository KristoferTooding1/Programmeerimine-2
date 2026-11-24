using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Operations
{
    public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteOperationCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Operations
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}
