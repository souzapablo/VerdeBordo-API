using System.Text.Json.Serialization;
using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;

namespace VerdeBordo.Application.Features.Orders.Commands.AddPaymentToOrder
{
    public class AddPaymentToOrderCommand : IRequest<PaymentVm?>
    {
        [JsonIgnore]
        public int OrderId { get; set; }   
        public decimal PayedAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}