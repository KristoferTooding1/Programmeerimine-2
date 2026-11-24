using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteCarCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            // Kõigepealt kustutame seotud tööd, siis kirje enda
            await _dbContext.Operations
                .Where(x => x.CarId == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _dbContext.Cars
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}
