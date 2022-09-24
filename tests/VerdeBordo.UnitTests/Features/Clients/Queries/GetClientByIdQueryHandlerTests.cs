using VerdeBordo.Application.Features.Clients.Queries.GetClientById;
using VerdeBordo.Application.Features.Clients.ViewModels;

namespace VerdeBordo.UnitTests.Features.Clients.Queries
{
    public class GetClientByIdQueryHandlerTests
    {
        private readonly GetClientByIdQueryHandler _queryHandler;
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();

        public GetClientByIdQueryHandlerTests()
        {
            _queryHandler = new(_clientRepositoryMock.Object, _messageHandlerMock.Object, _orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_AValidClientId_When_QueryIsExecuted_Should_ReturnClientDetails()
        {
            // Arrage
            Client client = new("Carlos", "(34) 98756-6546");
            GetClientByIdQuery query = new(1);

            _clientRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(client);

            _orderRepositoryMock.Setup(x => x.GetOrdersByClentIdAsync(1))
                .ReturnsAsync(client.Orders);

            // Act
            var result = await _queryHandler.Handle(query, new CancellationToken());

            // Assert
            result?.Name.Should().Be("Carlos");
            result?.Contact.Should().Be("(34) 98756-6546");
            result?.Should().BeOfType<ClientDetailsVm>();
        }

        [Fact]
        public async Task Given_AnInvalidClientId_When_QueryIsExecuted_Should_ReturnMessage()
        {
            // Arrage
            GetClientByIdQuery query = new(1);

            // Act
            var result = await _queryHandler.Handle(query, new CancellationToken());

            // Assert
            result?.Should().BeNull();
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == "Cliente com o Id 1 não encontrado.");
        }        
    }
}