using MediatR;
using VerdeBordo.Application.Features.Clients.ViewModels;
using VerdeBordo.Application.Features.Orders.ViewModels;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Enums;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Extensions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDetailsVm?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMessageHandler _messageHandler;
        private readonly IOrderRepository _orderRepository;

        public GetClientByIdQueryHandler(IClientRepository clientRepository, IMessageHandler messageHandler, 
            IOrderRepository orderRepository)
        {
            _clientRepository = clientRepository;
            _messageHandler = messageHandler;
            _orderRepository = orderRepository;
        }

        public async Task<ClientDetailsVm?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await Validate(request);

            if (client is null)
                return null;
                
            var orders = await _orderRepository.GetOrdersByClentIdAsync(request.ClientId);

            return new ClientDetailsVm
            {
                ClientId = client.Id,
                Name = client.Name,
                Contact = client.Contact,
                Orders = orders.Select(x => new OrderListVm
                {
                    OrderId = x.Id,
                    OrderDate = x.OrderDate,
                    OrderPrice = x.OrderPrice,
                    DeliveryFee = x.DeliveryFee,
                    Status = EnumExtensions<OrderStatus>.GetDescription(x.OrderStatus)
                }).ToList()
            };
        }

        private async Task<Client?> Validate(GetClientByIdQuery request)
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