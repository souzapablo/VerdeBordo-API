using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.API.Controllers.Responses;
using VerdeBordo.Application.Features.Orders.Queries.GetAllOrders;
using VerdeBordo.Application.Features.Orders.Queries.GetOrderById;
using VerdeBordo.Controllers.Responses;

namespace VerdeBordo.API.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var order = await _mediator.Send(new GetAllOrdersQuery());
            
            return CreateCustomResponse<SuccessResponse>(order);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order is null)
                return CreateCustomResponse<NotFoundResponse>(id);

            return CreateCustomResponse<SuccessResponse>(order);
        }
    }
}