using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;
using VerdeBordo.Core.Enums;
using VerdeBordo.Core.Extensions;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDetailsVm?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IClientRepository clientRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }

        public async Task<OrderDetailsVm?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, x => x.Payments);

            if (order is null)
                return null;
            
            var client = await _clientRepository.GetByIdAsync(order.ClientId);

            if (client is null)
                return null;

            return new OrderDetailsVm
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Embroideries = order.Embroideries,
                Payments = order.Payments,
                IsPromptDelivery = order.PromptDelivery,
                PayedAmount = order.PayedAmount,
                OrderStatus = EnumExtensions<OrderStatus>.GetDescription(order.OrderStatus),
                DeliveredAt = order.DeliveredAt,
                PaymentMethod = EnumExtensions<PaymentMethod>.GetDescription(order.PaymentMethod),
                DeliveryFee = order.DeliveryFee,
                ClientName = client.Name,
                IsDeleted = order.IsDeleted,
                OrderTotalValue = order.OrderPrice
            };
        }
    }
}