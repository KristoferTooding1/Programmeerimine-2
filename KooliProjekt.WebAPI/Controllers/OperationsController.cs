using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using KooliProjekt.Application.Features.Operations;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<OperationDto>>> List()
        {
            var result = await _mediator.Send(new OperationListQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new OperationGetQuery(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

