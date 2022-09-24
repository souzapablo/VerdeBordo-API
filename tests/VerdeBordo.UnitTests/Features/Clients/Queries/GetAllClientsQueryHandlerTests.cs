using VerdeBordo.Application.Features.Clients.Queries.GetAllClients;

namespace VerdeBordo.UnitTests.Features.Clients.Queries
{
    public class GetAllClientsQueryHandlerTests
    {
        private readonly GetAllClientsQueryHandler _queryHandler;
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();

        public GetAllClientsQueryHandlerTests()
        {
            _queryHandler = new(_clientRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_ThreeClientsInDataBase_When_QueryIsExecuted_Should_ReturnThreeClients()
        {
            // Assert
            var clients = new FakeClient().Generate(3);

            _clientRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(clients);

            // Act
            var result = await _queryHandler.Handle(new GetAllClientsQuery(), new CancellationToken());

            // Assert
            result.Should().HaveCount(3);
        }
    }
}