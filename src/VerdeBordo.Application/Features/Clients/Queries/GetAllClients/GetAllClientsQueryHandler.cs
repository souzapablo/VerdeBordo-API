using MediatR;
using VerdeBordo.Application.Features.Clients.ViewModels;
using VerdeBordo.Core.Interfaces.Repositories;


namespace VerdeBordo.Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientListVm>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientsQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientListVm>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients.Select(x => new ClientListVm
            {
                ClientId = x.Id,
                Name = x.Name,
                Contact = x.Contact,
                IsDeleted = x.IsDeleted
            }).ToList();
        }
    }
}