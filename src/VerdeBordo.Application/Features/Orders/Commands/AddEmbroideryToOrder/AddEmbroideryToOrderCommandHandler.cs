using System.Transactions;
using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Commands.AddEmbroideryToOrder
{
    public class AddEmbroideryToOrderCommandHandler : IRequestHandler<AddEmbroideryToOrderCommand, EmbroideryVm?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageHandler _messageHandler;
        private readonly IEmbroideryRepository _embroideryRepository;

        public AddEmbroideryToOrderCommandHandler(IOrderRepository orderRepository, IMessageHandler messageHandler, IEmbroideryRepository embroideryRepository)
        {
            _orderRepository = orderRepository;
            _messageHandler = messageHandler;
            _embroideryRepository = embroideryRepository;
        }

        public async Task<EmbroideryVm?> Handle(AddEmbroideryToOrderCommand request, CancellationToken cancellationToken)
        {
            using var transcation = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var order = await Validate(request);

            if (order is null)
                return null;

            Embroidery embroidery = new(request.Description, request.Price, request.OrderId);

            await _embroideryRepository.AddAsync(embroidery);

            order.AddEmbroidery(embroidery);

            await _orderRepository.UpdateAsync(order);

            transcation.Complete();
            
            return new EmbroideryVm
            {
                EmbroideryId = embroidery.Id,
                Description = embroidery.Description,
                Price = embroidery.Price
            };
        }

        private async Task<Order?> Validate(AddEmbroideryToOrderCommand request)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
            {
                var exception = new OrderNotFoundException(request.OrderId);
                _messageHandler.AddMessage("001", exception.Message);
                return null;
            }

            return order;
        }
    }
}