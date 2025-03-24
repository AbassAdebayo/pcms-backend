using Api.Filters;
using Application.Commands.Employer;
using Application.Commands.Member.CreateMemberCommand;
using Application.Models;
using Application.Queries.Employer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.MemberController
{
    [Route("api/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MemberController> _logger;

        public MemberController(IMediator mediator, ILogger<MemberController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Create([FromBody] CreateMemberCommand request)
        {
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

    }
}
