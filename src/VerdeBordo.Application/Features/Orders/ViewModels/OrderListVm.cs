namespace VerdeBordo.Application.Features.Orders.ViewModels
{
    public class OrderListVm
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; } 
        public DateTime OrderDate { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal? DeliveryFee { get; set; }
        public string Status { get; set; } = null!;
        public bool IsDeleted { get; set; }

    }
}