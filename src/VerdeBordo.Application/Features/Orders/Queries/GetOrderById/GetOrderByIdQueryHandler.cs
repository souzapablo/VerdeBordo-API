using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;
using VerdeBordo.Core.Enums;
using VerdeBordo.Core.Extensions;
using VerdeBordo.Core.Persistence.Interfaces;

namespace VerdeBordo.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDetailsVm?>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDetailsVm?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
                return null;
            
            return new OrderDetailsVm
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Embroideries = order.Embroideries,
                Payments = order.Payments,
                IsPromptDelivery = order.IsPromptDelivery,
                PayedAmount = order.PayedAmount,
                OrderStatus = EnumExtensions<OrderStatus>.GetDescription(order.OrderStatus),
                DeliveredAt = order.DeliveredAt,
                PaymentMethod = EnumExtensions<PaymentMethod>.GetDescription(order.PaymentMethod),
                DeliveryFee = order.DeliveryFee
            };
        }
    }
}