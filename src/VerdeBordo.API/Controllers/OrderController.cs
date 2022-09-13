using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.API.Controllers.Responses;
using VerdeBordo.Controllers.Responses;
using VerdeBordo.Core.Entities;

namespace VerdeBordo.API.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : BaseController
    {
        private static List<Order> orderList = new() { new Order(DateTime.Now, new Client("Cliente", "@cliente"), Core.Enums.PaymentMethod.BankTransfer, false) };

        [HttpGet]
        public IActionResult GetAll()
        {
            return CreateCustomResponse<SuccessResponse>(orderList);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = orderList.FirstOrDefault(x => x.Id == id);

            if (order is null)
                return CreateCustomResponse<NotFoundResponse>(id);

            return CreateCustomResponse<SuccessResponse>(order);
        }
    }
}