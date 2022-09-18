using VerdeBordo.Application.Features.Orders.ViewModels;

namespace VerdeBordo.Application.Features.Clients.ViewModels
{
    public class ClientDetailsVm
    {
        public ClientDetailsVm()
        {
            Orders = new();
        }

        public int ClientId { get; set; }
        public string Name { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public List<OrderListVm> Orders { get; set; }
    }
}