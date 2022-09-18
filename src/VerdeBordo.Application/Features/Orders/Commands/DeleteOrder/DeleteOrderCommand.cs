using MediatR;

namespace VerdeBordo.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
        
        public int OrderId { get; set; }
    }
}