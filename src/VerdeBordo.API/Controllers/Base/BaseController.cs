using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base.Interfaces;

namespace VerdeBordo.API.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CreateCustomResponse<T>(object result)
            where T : IBaseResponse, new() 
            {
                var response = new T();

                return response.CreateResponse(result);
            }    
    }
} 