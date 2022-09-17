using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.API.Controllers.Responses;
using VerdeBordo.Application.Features.Orders.Commands.PostOrder;
using VerdeBordo.Application.Features.Orders.Queries.GetAllOrders;
using VerdeBordo.Application.Features.Orders.Queries.GetOrderById;
using VerdeBordo.Controllers.Responses;

namespace VerdeBordo.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/orders")]
    [OpenApiTag("Order", Description = "Pedidos")]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
    #pragma warning restore

        /// <summary>
        /// Lista todos os pedidos
        /// </summary>
        /// <returns>Pedidos cadastrados</returns>
        /// <response code="200">Retorna os pedidos cadastrados</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var order = await _mediator.Send(new GetAllOrdersQuery());
            
            return CreateCustomResponse<SuccessResponse>(order);
        }

        /// <summary>
        /// Busca o pedido pelo Id
        /// </summary>
        /// <returns>Pedidos cadastrados</returns>
        /// <param name="orderId">Id do pedido a ser encontrado</param>
        /// <response code="200">Retorna o pedido encontrado</response>
        /// <response code="404">Pedido com o id informado não encontrado</response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int orderId)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(orderId));

            if (order is null)
                return CreateCustomResponse<NotFoundResponse>(orderId);

            return CreateCustomResponse<SuccessResponse>(order);
        }

        /// <summary>
        /// Criar um novo pedido
        /// </summary>
        /// <returns>Pedido criado</returns>
        /// <param name="command">Objeto contendo informações do pedido a ser criado</param>
        /// <response code="201">Pedido criado com sucesso</response>
        /// <response code="400">Informações inválidas</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostOrderAsync([FromBody] PostOrderCommand command)
        {
            var orderId = await _mediator.Send(command);

            if (orderId is null)
                return CreateCustomResponse<BadRequestResponse>(orderId.HasValue);

            return CreatedAtAction(nameof(GetById), new {OrderId = orderId}, command);
        }
    }
}