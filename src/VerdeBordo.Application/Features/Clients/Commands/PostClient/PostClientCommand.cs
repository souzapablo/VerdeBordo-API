using MediatR;

namespace VerdeBordo.Application.Features.Clients.Commands.PostClient
{
    public class PostClientCommand : IRequest<int?>
    {
        public string Name { get; set; } = null!;
        public string Contact { get; set; } = null!;
    }
}