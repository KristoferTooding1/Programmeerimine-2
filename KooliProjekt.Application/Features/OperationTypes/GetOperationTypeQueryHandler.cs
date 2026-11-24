using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.OperationTypes
{
    public class GetOperationTypeQueryHandler : IRequestHandler<GetOperationTypeQuery, OperationResult<OperationTypeDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetOperationTypeQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<OperationTypeDto>> Handle(GetOperationTypeQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<OperationTypeDto>();
            result.Value = await _dbContext.OperationTypes
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new OperationTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
