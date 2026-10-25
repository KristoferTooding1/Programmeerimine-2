using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using KooliProjekt.Application.Features.OperationTypes;

namespace KooliProjekt.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<OperationTypeDto>>> List()
        {
            var result = await _mediator.Send(new OperationTypeListQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDto>> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new OperationTypeGetQuery(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
