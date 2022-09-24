using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Commands.AddDeliveryFeeToOrder
{
    public class AddDeliveryFeeToOrderCommandHandler : IRequestHandler<AddDeliveryFeeToOrderCommand, DeliveryFeeVm?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageHandler _messageHandler;

        public AddDeliveryFeeToOrderCommandHandler(IOrderRepository orderRepository, IMessageHandler messageHandler)
        {
            _orderRepository = orderRepository;
            _messageHandler = messageHandler;
        }

        public async Task<DeliveryFeeVm?> Handle(AddDeliveryFeeToOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await Validate(request);

            if (order is null)
                return null;

            order.SetDeliveryFee(request.DeliveryFee);

            await _orderRepository.UpdateAsync(order);

            return new DeliveryFeeVm
            {
                DeliveryFee = order.DeliveryFee.GetValueOrDefault()
            };
        }

        private async Task<Order?> Validate(AddDeliveryFeeToOrderCommand request)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
            {
                var exception = new OrderNotFoundException(request.OrderId);
                _messageHandler.AddMessage("001", exception.Message);
                return null;
            }

            return order;
        }
    }
}