using MediatR;

namespace VerdeBordo.Application.Features.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest<Unit>
    {
        public DeleteClientCommand(int clientId)
        {
            ClientId = clientId;
        }

        public int ClientId { get; set; }
    }
}