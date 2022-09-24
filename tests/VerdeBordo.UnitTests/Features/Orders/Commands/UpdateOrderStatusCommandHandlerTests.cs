using VerdeBordo.Application.Features.Orders.Commands.UpdateOrderStatus;
using VerdeBordo.Core.Extensions;

namespace VerdeBordo.UnitTests.Features.Orders.Commands
{
    public class UpdateOrderStatusCommandHandlerTests
    {
        private readonly UpdateOrderStatusCommandHandler _commandHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();

        public UpdateOrderStatusCommandHandlerTests()
        {
            _commandHandler = new(_orderRepositoryMock.Object, _messageHandlerMock.Object);
        }

        [Fact]
        public async Task Given_AValidStatusForOrder_When_CommandIsExcecuted_Should_UpdateOrderStatus()
        {
            // Arrange
            Order order = new(DateTime.Now, 1, 1, PaymentMethod.PagSeguro, true);
            UpdateOrderStatusCommand command = new() 
            {
                 OrderId = 1, 
                 NewStatus = OrderStatus.Drafting 
            };

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.OrderStatus.Should().Be(OrderStatus.Drafting);
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Once);
        }

        [Fact]
        public async Task Given_DeliveredStatusAndStatusIsDelivering_When_CommandIsExcecuted_Should_UpdateOrderStatusAndDeliveredAt()
        {
            // Arrange
            Order order = new(DateTime.Now, 1, 1, PaymentMethod.PagSeguro, true);
            UpdateOrderStatusCommand command = new() 
            {
                 OrderId = 1, 
                 NewStatus = OrderStatus.Delivered,
                 DeliveredAt = new DateTime(2022, 12, 09) 
            };
            order.SetStatus(OrderStatus.Delivering);
            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.OrderStatus.Should().Be(OrderStatus.Delivered);
            order.DeliveredAt.Should().Be(new DateTime(2022, 12, 09));
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Once);
        }

        [Fact]
        public async Task Given_DeliveredStatusAndStatusIsNotDelivering_When_CommandIsExcecuted_Should_ReturnMessage()
        {
            // Arrange
            Order order = new(DateTime.Now, 1, 1, PaymentMethod.PagSeguro, true);
            UpdateOrderStatusCommand command = new() 
            {
                 OrderId = 1, 
                 NewStatus = OrderStatus.Delivered,
                 DeliveredAt = new DateTime(2022, 12, 09) 
            };

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.OrderStatus.Should().Be(OrderStatus.Created);
            order.DeliveredAt.Should().BeNull();
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == $"Status Criado inválido.");
        }
    }        
}