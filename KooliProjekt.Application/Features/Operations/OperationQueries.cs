using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Operations
{
    public class OperationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal? Cost { get; set; }
        public int CarId { get; set; }
        public int EmployeeId { get; set; }
        public int OperationTypeId { get; set; }
    }

    public class OperationListQuery : IRequest<List<OperationDto>> { }

    public class OperationListQueryHandler : IRequestHandler<OperationListQuery, List<OperationDto>>
    {
        private readonly ApplicationDbContext _context;
        public OperationListQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<List<OperationDto>> Handle(OperationListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Operations
                .Select(x => new OperationDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    Status = x.Status,
                    Cost = x.Cost,
                    CarId = x.CarId,
                    EmployeeId = x.EmployeeId,
                    OperationTypeId = x.OperationTypeId
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class OperationGetQuery : IRequest<OperationDto>
    {
        public int Id { get; }
        public OperationGetQuery(int id) => Id = id;
    }

    public class OperationGetQueryHandler : IRequestHandler<OperationGetQuery, OperationDto>
    {
        private readonly ApplicationDbContext _context;
        public OperationGetQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<OperationDto> Handle(OperationGetQuery request, CancellationToken cancellationToken)
        {
            return await _context.Operations
                .Where(x => x.Id == request.Id)
                .Select(x => new OperationDto
                {
                    Id = x.Id,
                    Date = x.Date,
                    Status = x.Status,
                    Cost = x.Cost,
                    CarId = x.CarId,
                    EmployeeId = x.EmployeeId,
                    OperationTypeId = x.OperationTypeId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
