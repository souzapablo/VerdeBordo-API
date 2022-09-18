using VerdeBordo.Application.Features.Orders.Commands.AddPaymentToOrder;

namespace VerdeBordo.UnitTests.Features.Orders.Commands
{
    public class AddPaymentToOrderCommandHandlerTests
    {
        private readonly AddPaymentToOrderCommandHandler _commandHandler;
        private readonly Mock<IOrderRepository> _orderRepoistoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock = new();

        public AddPaymentToOrderCommandHandlerTests()
        {
            _commandHandler = new(_orderRepoistoryMock.Object, _messageHandlerMock.Object, _paymentRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Given_AValidPaymentToAOrderPendingPayment_When_CommandIsExecuted_Should_AddPaymentToOrderAndIncreasePayedAmount()
        {
            // Arrange
            Order order = new Order(DateTime.Now, 1, PaymentMethod.PicPay, false);
            order.SetDeliveryFee(2m);
            AddPaymentToOrderCommand command = new()
            {
                OrderId = 1,
                PayedAmount = 2m,
                PaymentDate = new DateTime(2022, 9, 12)
            };

            _orderRepoistoryMock.Setup(x => x.GetByIdAsync(1, x => x.Payments))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.Payments.Count.Should().Be(1);
            order.Payments[0].PaymentDate.Should().Be(new DateTime(2022, 9, 12));
            order.PayedAmount.Should().Be(2m);
            _paymentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Payment>()), Times.Once);
            _orderRepoistoryMock.Verify(x => x.UpdateAsync(order), Times.Once);
        }

        [Fact]
        public async Task Given_AnInvalidOrderId_When_CommandIsExecuted_Should_ReturnMessage()
        {
            // Arrange
            Order order = new Order(DateTime.Now, 1, PaymentMethod.PicPay, false);

            AddPaymentToOrderCommand command = new()
            {
                OrderId = 1,
                PayedAmount = 2m,
                PaymentDate = new DateTime(2022, 9, 12)
            };

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.Payments.Count.Should().Be(0);
            order.PayedAmount.Should().Be(0);
            _paymentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Payment>()), Times.Never);
            _orderRepoistoryMock.Verify(x => x.UpdateAsync(order), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == $"Pedido com o Id {command.OrderId} não encontrado.");
        }

        [Fact]
        public async Task Given_TotallyPayedOrder_When_CommandIsExecuted_Should_ReturnAlreadyPayedMessage()
        {
            // Arrange
            Order order = new Order(DateTime.Now, 1, PaymentMethod.PicPay, false);
            order.SetDeliveryFee(2m);
            order.AddEmbroidery(new("Bordado", 2m));
            order.AddPayment(new(DateTime.Now, 4m, 1));

            AddPaymentToOrderCommand command = new()
            {
                OrderId = 1,
                PayedAmount = 2m,
                PaymentDate = new DateTime(2022, 9, 12)
            };

            _orderRepoistoryMock.Setup(x => x.GetByIdAsync(1, x => x.Payments))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _paymentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Payment>()), Times.Never);
            _orderRepoistoryMock.Verify(x => x.UpdateAsync(order), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == $"Valor totaldo pedido já foi pago.");
        }   

        [Fact]
        public async Task Given_PaymentAmountIsGreatherThanOrderPrice_When_CommandIsExecuted_Should_ReturnPaymentExceedsOrderPriceMessage()
        {
            // Arrange
            Order order = new Order(DateTime.Now, 1, PaymentMethod.PicPay, false);
            order.SetDeliveryFee(2m);
            order.AddEmbroidery(new("Bordado", 2m));
            order.AddPayment(new(DateTime.Now, 5m, 1));

            AddPaymentToOrderCommand command = new()
            {
                OrderId = 1,
                PayedAmount = 2m,
                PaymentDate = new DateTime(2022, 9, 12)
            };

            _orderRepoistoryMock.Setup(x => x.GetByIdAsync(1, x => x.Payments))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _paymentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Payment>()), Times.Never);
            _orderRepoistoryMock.Verify(x => x.UpdateAsync(order), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == $"Valor do pagamento excede o valor do pedido.");
        } 
    }
}