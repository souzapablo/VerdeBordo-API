using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base.Interfaces;

namespace VerdeBordo.Controllers.Responses
{
    public class SuccessResponse : IBaseResponse
    {
        public IActionResult CreateResponse(object result)
        {
            return new OkObjectResult(new 
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Data = result
            });
        }
    }
}