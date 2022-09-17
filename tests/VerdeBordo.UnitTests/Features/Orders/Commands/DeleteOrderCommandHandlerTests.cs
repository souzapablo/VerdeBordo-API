using VerdeBordo.Application.Features.Orders.Commands.DeleteOrder;

namespace VerdeBordo.UnitTests.Features.Orders.Commands
{
    public class DeleteOrderCommandHandlerTests
    {
        private readonly DeleteOrderCommandHandler _commandHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        public DeleteOrderCommandHandlerTests()
        {
            _commandHandler = new(_orderRepositoryMock.Object, _messageHandlerMock.Object);
        }

        [Fact]
        public async Task Given_AValidOrderId_When_CommandIsExecuted_Should_SetIsDeletedTrue()
        {
            // Arrange
            Order order = new(DateTime.Now, 1, PaymentMethod.BankTransfer, true);
            DeleteOrderCommand command = new(1);

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.IsDeleted.Should().BeTrue();
            _orderRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Given_AnInvalidOrderId_When_CommandIsExecuted_Should_AddMessageToHandler()
        {
            // Arrange
            DeleteOrderCommand command = new(1);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == $"Pedido com o Id {command.OrderId} não encontrado.");
        }

        [Fact]
        public async Task Given_ADeletedOrder_When_CommandIsExecuted_Should_ReturnMessage()
        {
            // Arrange
            Order order = new(DateTime.Now, 1, PaymentMethod.BankTransfer, true);
            DeleteOrderCommand command = new(1);
            order.SetIsDeleted(true);

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == "Pedido já foi apagado anteriormente.");
        }
    }
}