using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, OperationResult<CarDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCarQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<CarDto>> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CarDto>();
            result.Value = await _dbContext.Cars
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new CarDto
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    RegNum = x.RegNum
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
