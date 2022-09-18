using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base.Interfaces;

namespace VerdeBordo.API.Controllers.Responses
{
    public class NotFoundResponse : IBaseResponse
    {
        public IActionResult CreateResponse(object result)
        {
            return new NotFoundObjectResult(new
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"Pedido com Id {result} não encontrado."
            });
        }
    }
}