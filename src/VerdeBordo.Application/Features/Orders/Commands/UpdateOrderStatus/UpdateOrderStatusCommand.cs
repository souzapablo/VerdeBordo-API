using System.Text.Json.Serialization;
using MediatR;
using VerdeBordo.Core.Enums;

namespace VerdeBordo.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}