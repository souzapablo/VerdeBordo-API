using MediatR;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Clients.Commands.PostClient
{
    public class PostClientCommandHandler : IRequestHandler<PostClientCommand, int?>
    {
        private readonly IClientRepository _clientRepository;

        public PostClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int?> Handle(PostClientCommand request, CancellationToken cancellationToken)
        {
            Client client = new(request.Name, request.Contact);

            await _clientRepository.AddAsync(client);

            return client.Id;
        }
    }
}