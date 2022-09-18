using Microsoft.AspNetCore.Mvc;

namespace VerdeBordo.API.Controllers.Base.Interfaces
{
    public interface IBaseResponse
    {
        IActionResult CreateResponse(object result);
    }
}