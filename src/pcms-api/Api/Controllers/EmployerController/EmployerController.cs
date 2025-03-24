using Api.Filters;
using Application.Commands.Employer;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.EmployerController
{
    [Route("api/employer")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployerController> _logger;

        public EmployerController(IMediator mediator, ILogger<EmployerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Create([FromBody] CreateEmployerCommand request)
        {
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}
