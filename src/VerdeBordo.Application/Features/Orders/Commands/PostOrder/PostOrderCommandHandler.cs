using MediatR;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Commands.PostOrder
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, int?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMessageHandler _messageHandler;

        public PostOrderCommandHandler(IOrderRepository orderRepository, IClientRepository clientRepository, 
            IMessageHandler messageHandler)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _messageHandler = messageHandler;
        }

        public async Task<int?> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            await Validate(request);

            if (_messageHandler.HasMessage)
                return null;
                
            Order order = new(request.OrderDate, request.ClientId, request.PaymentMethod, request.IsPromptDelivery);

            if (request.DeliveryFee.HasValue)
                order.SetDeliveryFee(request.DeliveryFee.Value);

            await _orderRepository.AddAsync(order);

            return order.Id;
        }

        private async Task Validate(PostOrderCommand request)
        {
            var clientExist = await _clientRepository.ExistAsync(request.ClientId);

            if (!clientExist)
            {
                var exception = new ClientNotFoundException(request.ClientId);
                _messageHandler.AddMessage("001",exception.Message);
            }
        }
    }
}