using VerdeBordo.Application.Features.Orders.Commands.PostOrder;
using VerdeBordo.Infrastructure.Common;

namespace VerdeBordo.UnitTests.Features.Orders.Commands
{
    public class PostOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<IOrderRepository> _clientRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        private readonly PostOrderCommandHandler _commandHandler;

        public PostOrderCommandHandlerTests()
        {
            _commandHandler = new(_orderRepositoryMock.Object, _clientRepositoryMock.Object, _messageHandlerMock.Object);
        }

        [Fact]
        public async Task Given_AValidPostOrderCommand_When_CommandIsExecuted_Should_SaveOrderInDatabase()
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

        [Fact]
        public async Task Given_AnInvalidClientId_When_CommandIsExecuted_ReturnNullWithMessageError()
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
                .ReturnsAsync(false);

            // Act
            var result = await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _messageHandlerMock.Object.HasMessage.Should().BeTrue();
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == $"Cliente com o Id {command.ClientId} não encontrado.");
        }        
    }
}