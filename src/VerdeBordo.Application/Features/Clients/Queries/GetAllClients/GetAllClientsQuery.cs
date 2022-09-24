using MediatR;
using VerdeBordo.Application.Features.Clients.ViewModels;

namespace VerdeBordo.Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<List<ClientListVm>>
    {
        
    }
}