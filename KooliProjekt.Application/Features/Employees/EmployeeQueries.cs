using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class EmployeeListQuery : IRequest<List<EmployeeDto>> { }

    public class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, List<EmployeeDto>>
    {
        private readonly ApplicationDbContext _context;
        public EmployeeListQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<List<EmployeeDto>> Handle(EmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Role = x.Role
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class EmployeeGetQuery : IRequest<EmployeeDto>
    {
        public int Id { get; }
        public EmployeeGetQuery(int id) => Id = id;
    }

    public class EmployeeGetQueryHandler : IRequestHandler<EmployeeGetQuery, EmployeeDto>
    {
        private readonly ApplicationDbContext _context;
        public EmployeeGetQueryHandler(ApplicationDbContext context) => _context = context;

        public async Task<EmployeeDto> Handle(EmployeeGetQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Where(x => x.Id == request.Id)
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Role = x.Role
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
