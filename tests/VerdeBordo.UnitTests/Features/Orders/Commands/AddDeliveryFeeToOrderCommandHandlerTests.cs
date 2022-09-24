using VerdeBordo.Application.Features.Orders.Commands.AddDeliveryFeeToOrder;

namespace VerdeBordo.UnitTests.Features.Orders.Commands
{
    public class AddDeliveryFeeToOrderCommandHandlerTests
    {
        private readonly AddDeliveryFeeToOrderCommandHandler _commandHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        
        public AddDeliveryFeeToOrderCommandHandlerTests()
        {
            _commandHandler = new(_orderRepositoryMock.Object, _messageHandlerMock.Object);
        }

        [Fact]
        public async Task Given_AValidDeliveryFeeCommand_When_CommandIsExecuted_Should_ChangeDeliveryFee()
        {
            // Arrange
            Order order = new(DateTime.Now, 1, 2, PaymentMethod.Pix, false);
            AddDeliveryFeeToOrderCommand command = new()
            {
                OrderId = 1,
                DeliveryFee = 15m
            };

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.DeliveryFee.Should().Be(15m);
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Once);
        }

        [Fact]
        public async Task Given_AnInvalidOrder_When_CommandIsExecuted_Should_ReturnMessage()
        {
            // Arrange
            Order order = new(DateTime.Now, 1, 2, PaymentMethod.Pix, false);
            AddDeliveryFeeToOrderCommand command = new()
            {
                OrderId = 1,
                DeliveryFee = 15m
            };

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.DeliveryFee.Should().BeNull();
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == "Pedido com o Id 1 não encontrado.");
        }
    }
}