using System.Transactions;
using MediatR;
using VerdeBordo.Application.Features.Orders.ViewModels;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Exceptions;
using VerdeBordo.Core.Interfaces.Messages;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Commands.AddPaymentToOrder
{
    public class AddPaymentToOrderCommandHandler : IRequestHandler<AddPaymentToOrderCommand, PaymentVm?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageHandler _messageHandler;
        private readonly IPaymentRepository _paymentRepository;

        public AddPaymentToOrderCommandHandler(IOrderRepository orderRepository, IMessageHandler messageHandler, 
            IPaymentRepository paymentRepository)
        {
            _orderRepository = orderRepository;
            _messageHandler = messageHandler;
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentVm?> Handle(AddPaymentToOrderCommand request, CancellationToken cancellationToken)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var order = await Validate(request);

            if (order is null)
                return null;

            Payment payment = new(request.PaymentDate, request.PayedAmount, order.Id);

            await _paymentRepository.AddAsync(payment);

            order.AddPayment(payment);

            await _orderRepository.UpdateAsync(order);

            transaction.Complete();

            return new PaymentVm
            {
                PaymentId = payment.Id,
                PaymentAmount = payment.PaymentValue,
                PaymentDate = payment.PaymentDate
            };
        }

        private async Task<Order?> Validate(AddPaymentToOrderCommand request)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, x => x.Payments);

            if (order is null)
            {
                var exception = new OrderNotFoundException(request.OrderId);
                _messageHandler.AddMessage("001", exception.Message);
                return null;
            }

            if (order.PayedAmount == order.OrderPrice)
            {
                _messageHandler.AddMessage("002", "Valor total do pedido já foi pago.");
                return null;
            }

            if (order.PayedAmount + request.PayedAmount > order.OrderPrice)
            {
                _messageHandler.AddMessage("003", "Valor do pagamento excede o valor do pedido.");
                return null;
            }

            return order;
        }
    }
}