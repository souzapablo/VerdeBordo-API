using System.Text.Json.Serialization;
using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;

namespace VerdeBordo.Application.Features.Orders.Commands.AddDeliveryFeeToOrder
{
    public class AddDeliveryFeeToOrderCommand : IRequest<DeliveryFeeVm?>
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        public decimal DeliveryFee { get; set; }
    }
}