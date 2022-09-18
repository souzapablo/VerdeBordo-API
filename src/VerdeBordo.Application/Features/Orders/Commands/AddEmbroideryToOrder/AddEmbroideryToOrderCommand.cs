using System.Text.Json.Serialization;
using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;

namespace VerdeBordo.Application.Features.Orders.Commands.AddEmbroideryToOrder
{
    public class AddEmbroideryToOrderCommand : IRequest<EmbroideryVm?>
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}