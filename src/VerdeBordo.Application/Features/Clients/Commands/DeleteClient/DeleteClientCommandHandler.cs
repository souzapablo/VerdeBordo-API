using MediatR;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Clients.Commands.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMessageHandler _messageHandler;
        public DeleteClientCommandHandler(IClientRepository clientRepository, IMessageHandler messageHandler)
        {
            _clientRepository = clientRepository;
            _messageHandler = messageHandler;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await Validate(request);

            if (client is null)
                return Unit.Value;

            client.SetIsDeleted(true);

            await _clientRepository.UpdateAsync(client);

            return Unit.Value;
        }

        private async Task<Client?> Validate(DeleteClientCommand request)
        {
            var client = await _clientRepository.GetByIdAsync(request.ClientId);

            if (client is null)
            {
                var exception = new ClientNotFoundException(request.ClientId);
                _messageHandler.AddMessage("001", exception.Message);
                return null;
            }

            if (client.IsDeleted)
            {
                _messageHandler.AddMessage("002", "Cliente já foi apagado anteriormente.");
                return null;
            }

            return client;
        }
    }
}