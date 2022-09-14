using VerdeBordo.Core.Entities;

namespace VerdeBordo.Application.Features.Orders.ViewModels
{
    public class OrderDetailsVm
    {
        public OrderDetailsVm()
        {
            Embroideries = new();
            Payments = new();
        }

        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ClientName { get; set; } = null!;
        public List<Embroidery> Embroideries { get; set; }
        public List<Payment> Payments { get; set; }
        public bool IsPromptDelivery { get; set; }
        public decimal PayedAmount { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime? DeliveredAt { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public decimal? DeliveryFee { get; set; }
    }
}