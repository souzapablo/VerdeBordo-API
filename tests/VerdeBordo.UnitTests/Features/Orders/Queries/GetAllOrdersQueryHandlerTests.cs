using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.UnitTests.Features.Orders.Queries
{
    public class GetAllOrdersQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly GetAllOrdersQueryHandler _queryHandler;
        
        public GetAllOrdersQueryHandlerTests()
        {
            _queryHandler = new(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Give_ThreeOrdersInDataBase_When_QueryIsExecuted_Should_ReturnThreeOrders()
        {
            // Arrange
            List<Order> orderList = new FakeOrder().Generate(3);
            GetAllOrdersQuery query = new();

            _orderRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(orderList);

            // Act
            var result = await _queryHandler.Handle(query, new CancellationToken());

            // Assert
            result.Should().HaveCount(3);
        }   
    }
}