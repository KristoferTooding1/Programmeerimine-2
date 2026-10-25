using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.OperationTypes
{
    public class OperationTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class OperationTypeListQuery : IRequest<List<OperationTypeDto>> { }

    public class OperationTypeListQueryHandler : IRequestHandler<OperationTypeListQuery, List<OperationTypeDto>>
    {
        private readonly ApplicationDbContext _context;
        public OperationTypeListQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<List<OperationTypeDto>> Handle(OperationTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.OperationTypes
                .Select(x => new OperationTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class OperationTypeGetQuery : IRequest<OperationTypeDto>
    {
        public int Id { get; }
        public OperationTypeGetQuery(int id) => Id = id;
    }

    public class OperationTypeGetQueryHandler : IRequestHandler<OperationTypeGetQuery, OperationTypeDto>
    {
        private readonly ApplicationDbContext _context;
        public OperationTypeGetQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<OperationTypeDto> Handle(OperationTypeGetQuery request, CancellationToken cancellationToken)
        {
            return await _context.OperationTypes
                .Where(x => x.Id == request.Id)
                .Select(x => new OperationTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
