namespace VerdeBordo.Application.Features.Orders.ViewModels
{
    public class EmbroideryVm
    {
        public int EmbroideryId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}