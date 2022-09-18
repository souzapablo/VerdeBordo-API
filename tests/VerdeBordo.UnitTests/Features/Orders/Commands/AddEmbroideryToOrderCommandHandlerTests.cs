using VerdeBordo.Application.Features.Orders.Commands.AddEmbroideryToOrder;

namespace VerdeBordo.UnitTests.Features.Orders.Commands
{
    public class AddEmbroideryToOrderCommandHandlerTests
    {
        private readonly AddEmbroideryToOrderCommandHandler _commandHandler;
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        private readonly Mock<IEmbroideryRepository> _embroideryRepositoryMock = new();

        public AddEmbroideryToOrderCommandHandlerTests()
        {
            _commandHandler = new(_orderRepositoryMock.Object, _messageHandlerMock.Object, _embroideryRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_AValidEmbroidery_When_CommandIsExcecuted_Should_AddEmbroideryToOrder()
        {
            // Assert
            Order order = new(DateTime.Now, 1, PaymentMethod.PicPay, true);
            AddEmbroideryToOrderCommand command = new()
            {
                OrderId = 1,
                Description = "Bordado 16cm com flores",
                Price = 240m
            };

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);
            
            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.Embroideries.Should().HaveCount(1);
            order.OrderPrice.Should().Be(240m);
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Once);
            _embroideryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Embroidery>()), Times.Once);
        }

        [Fact]
        public async Task Given_AnInalidOrder_When_CommandIsExcecuted_Should_ReturnMessage()
        {
            // Assert
            Order order = new(DateTime.Now, 1, PaymentMethod.PicPay, true);
            AddEmbroideryToOrderCommand command = new()
            {
                OrderId = 1,
                Description = "Bordado 16cm com flores",
                Price = 240m
            };

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            order.Embroideries.Should().HaveCount(0);
            order.OrderPrice.Should().Be(0);
            _orderRepositoryMock.Verify(x => x.UpdateAsync(order), Times.Never);
            _embroideryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Embroidery>()), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == "Pedido com o Id 1 não encontrado.");
        }
    }
}