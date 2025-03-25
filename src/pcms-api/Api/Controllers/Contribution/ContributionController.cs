using Api.Filters;
using Application.Commands.Contribution.CreateContributionCommand;
using Application.Commands.Member.CreateMemberCommand;
using Application.Models;
using Application.Queries.Contribution.GetContributionInterestQuery;
using Application.Queries.Contribution.GetMemberTotalContributionsQuery;
using Application.Queries.Contribution.ListMemberContributionsQuery;
using Application.Queries.Member.ListMembersByEmployerIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.Contribution
{
    [Route("api/contributions")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContributionController> _logger;

        public ContributionController(IMediator mediator, ILogger<ContributionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Create([FromBody] CreateContributionCommand request)
        {
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpGet("statement/{memberId}/{page}/{pageSize}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GenerateMemberContributionStatement([FromRoute] Guid memberId, [FromRoute] int page, [FromRoute] int pageSize)
        {
            var request = new ListMemberContributionsQuery(memberId, page, pageSize);
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpGet("total/{memberId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GenerateMemberTotalContributions([FromRoute] Guid memberId)
        {
            var request = new GetMemberTotalContributionsQuery(memberId);
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpGet("interest/{memberId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> CalculateContributionInterest([FromRoute] Guid memberId)
        {
            var request = new GetContributionInterestQuery(memberId);
            var response = await _mediator.Send(request);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}
