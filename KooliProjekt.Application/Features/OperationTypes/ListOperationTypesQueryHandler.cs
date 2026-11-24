using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.OperationTypes
{
    public class ListOperationTypesQueryHandler : IRequestHandler<ListOperationTypesQuery, OperationResult<PagedResult<OperationTypeDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListOperationTypesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<OperationTypeDto>>> Handle(ListOperationTypesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<OperationTypeDto>>();
            result.Value = await _dbContext.OperationTypes
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new OperationTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
