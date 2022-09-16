using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using VerdeBordo.API.Controllers.Base.Interfaces;
using VerdeBordo.Core.Interfaces.Messages;

namespace VerdeBordo.API.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CreateCustomResponse<T>(object result)
            where T : IBaseResponse, new()
        {
            var messageHandler = HttpContext is not null ? HttpContext.RequestServices.GetService<IMessageHandler>() : default;

            if (messageHandler?.HasMessage == true)
            {
                return BadRequest(new
                {
                    Succes = false,
                    Status = HttpStatusCode.BadRequest,
                    Message = messageHandler.Messages
                                .Select(x => x.Value)
                });
            }
            var response = new T();

            return response.CreateResponse(result);
        }
    }
}