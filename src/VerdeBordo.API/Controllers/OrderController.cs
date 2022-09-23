using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.API.Controllers.Responses;
using VerdeBordo.Application.Features.Orders.Commands.AddDeliveryFeeToOrder;
using VerdeBordo.Application.Features.Orders.Commands.AddEmbroideryToOrder;
using VerdeBordo.Application.Features.Orders.Commands.AddPaymentToOrder;
using VerdeBordo.Application.Features.Orders.Commands.DeleteOrder;
using VerdeBordo.Application.Features.Orders.Commands.PostOrder;
using VerdeBordo.Application.Features.Orders.Commands.UpdateOrderStatus;
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

        /// <summary>
        /// Lista todos os pedidos
        /// </summary>
        /// <returns>Pedidos cadastrados</returns>
        /// <response code="200">Retorna os pedidos cadastrados</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            
            return CreateCustomResponse<SuccessResponse>(orders);
        }

        /// <summary>
        /// Busca o pedido pelo Id
        /// </summary>
        /// <returns>Detalhes do pedido</returns>
        /// <param name="orderId">Id do pedido a ser encontrado</param>
        /// <response code="200">Retorna os detalhes do pedido encontrado</response>
        /// <response code="404">Pedido com o Id informado não encontrado</response>
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

        /// <summary>
        /// Deleta logicamente um pedido
        /// </summary>
        /// <returns></returns>
        /// <param name="orderId">Id do pedido a ser deletado</param>
        /// <response code="204">Pedido deletado com sucesso</response>
        /// <response code="400">Pedido não encontrado ou já havia sido deletado </response>
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrderAsync(int orderId)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(orderId));

            return CreateCustomResponse<NoContentResponse>(result);
        }

        /// <summary>
        /// Atualiza status de um pedido
        /// </summary>
        /// <returns></returns>
        /// <param name="orderId">Id do pedido a ser atualiazdo</param>
        /// <param name="command">Objeto para atualização do pedido contendo novo status e data de entrega, quando necessária</param>
        /// <response code="204">Pedido atualizado com sucesso</response>
        /// <response code="400">Atualização de pedido não realizada </response>
        [HttpPatch("{orderId}/update-status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderStatusAsync(int orderId, [FromBody] UpdateOrderStatusCommand command)
        {
            command.OrderId = orderId;

            var result = await _mediator.Send(command);

            return CreateCustomResponse<NoContentResponse>(result);
        }

        /// <summary>
        /// Adiciona um pagamento ao pedido
        /// </summary>
        /// <returns>Objeto do pagamento adicionado</returns>
        /// <param name="orderId">Id do pedido a ser atualiazdo</param>
        /// <param name="command">Objeto para atualização do pedido contendo data e valor do pagamento</param>
        /// <response code="204">Pedido atualizado com sucesso</response>
        /// <response code="400">Atualização de pedido não realizada </response>
        [HttpPatch("{orderId}/add-payment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPaymentToOrderAsync(int orderId, [FromBody] AddPaymentToOrderCommand command)
        {
            command.OrderId = orderId;

            var result = await _mediator.Send(command);

            if (result is null)
                return CreateCustomResponse<BadRequestResponse>(orderId);

            return CreateCustomResponse<SuccessResponse>(result);
        }

        /// <summary>
        /// Adiciona um bordado ao pedido
        /// </summary>
        /// <returns>Objeto do bordado adicionado</returns>
        /// <param name="orderId">Id do pedido a ser atualiazdo</param>
        /// <param name="command">Objeto para atualização do pedido com descrição e valor do bordado</param>
        /// <response code="204">Pedido atualizado com sucesso</response>
        /// <response code="400">Atualização de pedido não realizada </response>
        [HttpPatch("{orderId}/add-embroidery")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmbroideryToOrderAsync(int orderId, [FromBody] AddEmbroideryToOrderCommand command)
        {
            command.OrderId = orderId;

            var result = await _mediator.Send(command);

            if (result is null)
                return CreateCustomResponse<BadRequestResponse>(orderId);

            return CreateCustomResponse<SuccessResponse>(result);
        }

        /// <summary>
        /// Adiciona valor da entrega ao pedido
        /// </summary>
        /// <returns>Valor da entrega</returns>
        /// <param name="orderId">Id do pedido a ser atualiazdo</param>
        /// <param name="command">Objeto para atualização do pedido com o valor da entrega</param>
        /// <response code="204">Pedido atualizado com sucesso</response>
        /// <response code="400">Atualização de pedido não realizada </response>
        [HttpPatch("{orderId}/add-delivery-fee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDeliveryFeeToOrderAsync(int orderId, [FromBody] AddDeliveryFeeToOrderCommand command)
        {
            command.OrderId = orderId;

            var result = await _mediator.Send(command);

            if (result is null)
                return CreateCustomResponse<BadRequestResponse>(orderId);

            return CreateCustomResponse<SuccessResponse>(result);
        }
    }
}