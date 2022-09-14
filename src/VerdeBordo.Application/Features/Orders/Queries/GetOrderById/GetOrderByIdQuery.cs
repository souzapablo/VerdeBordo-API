using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;

namespace VerdeBordo.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDetailsVm?>
    {
        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}