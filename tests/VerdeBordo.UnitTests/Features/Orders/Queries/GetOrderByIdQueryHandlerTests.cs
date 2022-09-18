using VerdeBordo.Application.Features.Orders.Queries.GetOrderById;
using VerdeBordo.Application.Features.Orders.ViewModels;

namespace VerdeBordo.UnitTests.Features.Orders.Queries
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly GetOrderByIdQueryHandler _queryHandler;

        public GetOrderByIdQueryHandlerTests()
        {
            _queryHandler = new(_orderRepositoryMock.Object, _clientRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_AValidOrderId_When_QueryIsExecuted_Should_ReturnOrderDetails()
        {
            // Arrange
            var query = new GetOrderByIdQuery(1);
            var order = new Order(new DateTime(2022, 12, 1), 2, PaymentMethod.PicPay, true);
            order.SetDeliveryFee(2m);
            var client = new Client("Nelson", "@nelson");

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(1, x => x.Payments, x => x.Embroideries))
                .ReturnsAsync(order);

            _clientRepositoryMock.Setup(x => x.GetByIdAsync(2))
                .ReturnsAsync(client);

            // Act
            var result = await _queryHandler.Handle(query, new CancellationToken());

            // Assert
            result.Should().BeOfType<OrderDetailsVm>();
            result?.OrderDate.Should().Be(new DateTime(2022, 12, 1));
            result?.ClientName.Should().Be("Nelson");
            result?.PaymentMethod.Should().Be("PicPay");
            result?.IsPromptDelivery.Should().Be(true);
            result?.DeliveryFee.Should().Be(2m);
            result?.OrderStatus.Should().Be("Criado");
            result?.OrderTotalValue.Should().Be(2m);
        }

        [Fact]
        public async Task Given_AnInvalidOrderId_When_QueryIsExecuted_Should_ReturnNull()
        {
            // Arrange
            var query = new GetOrderByIdQuery(1);

            // Act
            var result = await _queryHandler.Handle(query, new CancellationToken());

            // Assert
            result.Should().BeNull();
        }
    }
}