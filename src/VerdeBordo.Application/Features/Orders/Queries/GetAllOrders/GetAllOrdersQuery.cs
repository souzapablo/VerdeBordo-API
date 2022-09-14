using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;

namespace VerdeBordo.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<List<OrderListVm>>
    {
        
    }
}