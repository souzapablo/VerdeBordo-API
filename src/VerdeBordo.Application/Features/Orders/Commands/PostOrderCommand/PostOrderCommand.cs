using MediatR;
using VerdeBordo.Core.Enums;

namespace VerdeBordo.Application.Features.Orders.Commands.PostOrderCommand
{
    public class PostOrderCommand: IRequest<int?>
    {
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsPromptDelivery { get; set; }
        public decimal? DeliveryFee { get; set; }
    }
}