using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.OperationTypes
{
    public class DeleteOperationTypeCommandHandler : IRequestHandler<DeleteOperationTypeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteOperationTypeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteOperationTypeCommand request, CancellationToken cancellationToken)
        {
            // Kõigepealt kustutame seotud tööd, siis kirje enda
            await _dbContext.Operations
                .Where(x => x.OperationTypeId == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _dbContext.OperationTypes
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}
