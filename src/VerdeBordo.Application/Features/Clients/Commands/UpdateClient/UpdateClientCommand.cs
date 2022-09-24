using System.Text.Json.Serialization;
using MediatR;
using VerdeBordo.Application.Features.Clients.ViewModels;

namespace VerdeBordo.Application.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest<ClientDetailsVm?>
    {
        [JsonIgnore]
        public int ClientId { get; set; }
        public string? NewName { get; set; }
        public string? NewContact { get; set; }
    }
}