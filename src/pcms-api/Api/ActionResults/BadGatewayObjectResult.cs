using Microsoft.AspNetCore.Mvc;

namespace Api.ActionResults
{
    public class BadGatewayObjectResult : ObjectResult
    {
        public BadGatewayObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status502BadGateway;
        }
    }
}
