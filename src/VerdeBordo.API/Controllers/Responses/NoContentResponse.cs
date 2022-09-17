using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base.Interfaces;

namespace VerdeBordo.API.Controllers.Responses
{
    public class NoContentResponse : IBaseResponse
    {
        public IActionResult CreateResponse(object result)
        {
            return new ObjectResult(new
            {
                Success = true,
                StatusCode = StatusCodes.Status204NoContent
            });
        }
    }
}