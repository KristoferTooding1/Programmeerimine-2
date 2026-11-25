using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Cars
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileClient _fileClient;

        public AddFileCommandHandler(ApplicationDbContext context, IFileClient fileClient)
        {
            _context = context;
            _fileClient = fileClient;
        }

        public async Task<OperationResult> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var car = await _context.FindAsync<Car>(request.Id);
            if (car == null)
            {
                return result.AddError("Cannot find Car with ID " + request.Id);
            }

            await _fileClient.Save(request.FileName, FileStore.Photos, request.InputStream);

            return result;
        }
    }
}
