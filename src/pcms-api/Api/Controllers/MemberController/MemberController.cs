using Api.Filters;
using Application.Commands.Employer;
using Application.Commands.Member.CreateMemberCommand;
using Application.Commands.Member.DeleteMemberCommand;
using Application.Models;
using Application.Queries.Employer;
using Application.Queries.Member.GetMemberByIdQuery;
using Application.Queries.Member.ListMembersByEmployerIdQuery;
using Application.Queries.Member.ListMembersQuery;
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


        [HttpGet("id/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var request = new GetMemberByIdQuery(Id);
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpPost("id/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var request = new DeleteMemberCommand(Id);
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{page}/{pageSize}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> List([FromRoute] int page, [FromRoute] int pageSize)
        {
            var request = new ListMembersQuery(page, pageSize);
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{employerId}/{page}/{pageSize}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> ListByEmployer([FromRoute] Guid employerId, [FromRoute] int page, [FromRoute] int pageSize)
        {
            var request = new ListMembersByEmployerIdQuery(employerId, page, pageSize);
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}
