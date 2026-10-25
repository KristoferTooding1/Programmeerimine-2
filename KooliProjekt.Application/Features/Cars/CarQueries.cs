using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Cars
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegNum { get; set; }
    }

    public class CarListQuery : IRequest<List<CarDto>> { }

    public class CarListQueryHandler : IRequestHandler<CarListQuery, List<CarDto>>
    {
        private readonly ApplicationDbContext _context;
        public CarListQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<List<CarDto>> Handle(CarListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Cars
                .Select(x => new CarDto
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    RegNum = x.RegNum
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class CarGetQuery : IRequest<CarDto>
    {
        public int Id { get; }
        public CarGetQuery(int id) => Id = id;
    }

    public class CarGetQueryHandler : IRequestHandler<CarGetQuery, CarDto>
    {
        private readonly ApplicationDbContext _context;
        public CarGetQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<CarDto> Handle(CarGetQuery request, CancellationToken cancellationToken)
        {
            return await _context.Cars
                .Where(x => x.Id == request.Id)
                .Select(x => new CarDto
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    RegNum = x.RegNum
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
