using VerdeBordo.Application.Features.Clients.Commands.PostClient;

namespace VerdeBordo.UnitTests.Features.Clients.Commands
{
    public class PostClientCommandHandlerTests
    {
        private readonly PostClientCommandHandler _commandHandler;
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();

        public PostClientCommandHandlerTests()
        {
            _commandHandler = new(_clientRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_AValidPostClientModel_When_CommandIsExecuted_Should_SaveClientInDatabase()
        {
            // Arrange
            PostClientCommand command = new()
            {
                Name = "Arlindo",
                Contact = "(34) 97832-4624"
            };

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _clientRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Client>()), Times.Once);
        }
    }
}