using MediatR;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageHandler _messageHandler;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMessageHandler messageHandler)
        {
            _orderRepository = orderRepository;
            _messageHandler = messageHandler;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await Validate(request);

            if (order is null)
                return Unit.Value;
            
            order.SetIsDeleted(true);

            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }

        private async Task<Order?> Validate(DeleteOrderCommand request)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
            {
                var exception = new OrderNotFoundException(request.OrderId);
                _messageHandler.AddMessage("001", exception.Message);
                return null;
            }

            if (order.IsDeleted)
            {
                _messageHandler.AddMessage("002", "Pedido já foi apagado anteriormente.");
                return null;
            }

            return order;
        }
    }
}