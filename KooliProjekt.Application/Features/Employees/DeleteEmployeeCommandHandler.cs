using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Employees
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteEmployeeCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Kõigepealt kustutame seotud tööd, siis kirje enda
            await _dbContext.Operations
                .Where(x => x.EmployeeId == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _dbContext.Employees
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}
