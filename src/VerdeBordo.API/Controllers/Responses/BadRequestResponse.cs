using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base.Interfaces;

namespace VerdeBordo.API.Controllers.Responses
{
    public class BadRequestResponse : IBaseResponse
    {
        public IActionResult CreateResponse(object result)
        {
            return new BadRequestObjectResult(result);
        }
    }
}
