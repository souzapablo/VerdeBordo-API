using MediatR;
using VerdeBordo.Application.Features.Clients.ViewModels;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDetailsVm?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMessageHandler _messageHandler;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IMessageHandler messageHandler)
        {
            _clientRepository = clientRepository;
            _messageHandler = messageHandler;
        }

        public async Task<ClientDetailsVm?> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await Validate(request);

            if (client is null)
                return null;
                
            if(!string.IsNullOrWhiteSpace(request.NewName))
                client.SetName(request.NewName);

            if(!string.IsNullOrWhiteSpace(request.NewContact))
                client.SetContact(request.NewContact);
            
            await _clientRepository.UpdateAsync(client);

            return new ClientDetailsVm
            {
                ClientId = client.Id,
                Name = client.Name,
                Contact = client.Contact
            };
        }

        private async Task<Client?> Validate(UpdateClientCommand request)
        {
            var client = await _clientRepository.GetByIdAsync(request.ClientId);

            if (client is null)
            {
                var exception = new ClientNotFoundException(request.ClientId);
                _messageHandler.AddMessage("001", exception.Message);
                return null;
            }

            return client;
        }
    }
}