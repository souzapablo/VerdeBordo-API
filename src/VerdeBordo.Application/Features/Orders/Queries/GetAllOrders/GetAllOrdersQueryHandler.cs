using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;
using VerdeBordo.Core.Enums;
using VerdeBordo.Core.Extensions;
using VerdeBordo.Core.Persistence.Interfaces;

namespace VerdeBordo.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderListVm>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderListVm>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(x => new OrderListVm
            {
                OrderId = x.Id,
                ClientName = x.Client != null ? x.Client.Name : string.Empty,
                OrderDate = x.OrderDate,
                OrderPrice = x.OrderPrice,
                DeliveryFee = x.DeliveryFee,
                Status = EnumExtensions<OrderStatus>.GetDescription(x.OrderStatus)
            }).ToList();
        }
    }
}