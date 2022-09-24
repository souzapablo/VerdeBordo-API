using MediatR;
using VerdeBordo.Application.Features.Clients.ViewModels;

namespace VerdeBordo.Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<ClientDetailsVm?>
    {
        public GetClientByIdQuery(int clientId)
        {
            ClientId = clientId;
        }

        public int ClientId { get; set; }
    }
}