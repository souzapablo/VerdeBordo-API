using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.API.Controllers.Responses;
using VerdeBordo.Application.Features.Orders.Commands.PostOrderCommand;
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
        public async Task<IActionResult> GetAllAsync()
        {
            var order = await _mediator.Send(new GetAllOrdersQuery());
            
            return CreateCustomResponse<SuccessResponse>(order);
        }

        
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(orderId));

            if (order is null)
                return CreateCustomResponse<NotFoundResponse>(orderId);

            return CreateCustomResponse<SuccessResponse>(order);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrderAsync([FromBody] PostOrderCommand command)
        {
            var orderId = await _mediator.Send(command);

            if (orderId is null)
                return CreateCustomResponse<NotFoundResponse>(orderId.HasValue);

            return CreatedAtAction(nameof(GetById), new {OrderId = orderId}, command);
        }
    }
}