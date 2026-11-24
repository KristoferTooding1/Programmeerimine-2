using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Cars
{
    public class ListCarsQueryHandler : IRequestHandler<ListCarsQuery, OperationResult<PagedResult<CarDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCarsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<CarDto>>> Handle(ListCarsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<CarDto>>();
            result.Value = await _dbContext.Cars
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(x => new CarDto
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    RegNum = x.RegNum
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
