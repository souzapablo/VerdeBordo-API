using VerdeBordo.Application.Features.Orders.Commands.PostOrderCommand;
using VerdeBordo.Core.Interfaces.Repositories;
using VerdeBordo.Infrastructure.Common;

namespace VerdeBordo.UnitTests.Features.Orders.Commands
{
    public class PostOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        private readonly PostOrderCommandHandler _commandHandler;

        public PostOrderCommandHandlerTests()
        {
            _commandHandler = new(_orderRepositoryMock.Object, _clientRepositoryMock.Object, _messageHandlerMock.Object);
        }

        [Fact]
        public async Task Given_AValidPostOrderCommand_When_CommandIsExecuted_OrderIsSavedInDatabase()
        {
            // Arrange
            PostOrderCommand command = new()
            {
                OrderDate = DateTime.Now,
                ClientId = 1,
                PaymentMethod = PaymentMethod.BankTransfer,
                IsPromptDelivery = false,
                DeliveryFee = 24m
            };

            _clientRepositoryMock.Setup(x => x.ExistAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}