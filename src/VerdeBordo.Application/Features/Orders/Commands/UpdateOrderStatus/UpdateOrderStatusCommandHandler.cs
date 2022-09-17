using MediatR;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Enums;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageHandler _messageHandler;

        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository, IMessageHandler messageHandler)
        {
            _orderRepository = orderRepository;
            _messageHandler = messageHandler;
        }

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await Validate(request);

            if (order is null)
                return Unit.Value;
            
            if (request.DeliveredAt.HasValue)
                order.DeliverOrder(request.DeliveredAt.Value);
            else 
                order.SetStatus(request.NewStatus);

            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }

        private async Task<Order?> Validate(UpdateOrderStatusCommand request)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
            {
                var exception = new OrderNotFoundException(request.OrderId);
                _messageHandler.AddMessage("001", exception.Message);
                return null;
            }

            if (request.NewStatus == OrderStatus.Delivered && order.OrderStatus != OrderStatus.Delivering)
            {
                var exception = new InvalidStatusException(order.OrderStatus);
                _messageHandler.AddMessage("002", exception.Message);
                return null;
            }

            return order;
        }
    }
}