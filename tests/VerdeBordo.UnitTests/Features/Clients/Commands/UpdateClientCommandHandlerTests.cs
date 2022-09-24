using VerdeBordo.Application.Features.Clients.Commands.UpdateClient;

namespace VerdeBordo.UnitTests.Features.Clients.Commands
{
    public class UpdateClientCommandHandlerTests
    {
        private readonly UpdateClientCommandHandler _commandHandler;
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();

        public UpdateClientCommandHandlerTests()
        {
            _commandHandler = new(_clientRepositoryMock.Object, _messageHandlerMock.Object);
        }

        [Fact]
        public async Task Given_AValidCommandHandler_When_CommandIsExecuted_Should_UpdateClientFields()
        {
            // Arrange
            Client client = new("Carlos", "@carlos");
            UpdateClientCommand command = new()
            {
                ClientId = 1,
                NewName = "Pablo",
                NewContact = "@szpbl"
            };

            _clientRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(client);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            client.Name.Should().Be("Pablo");
            client.Contact.Should().Be("@szpbl");
            _clientRepositoryMock.Verify(x => x.UpdateAsync(client), Times.Once);
        }

        [Fact]
        public async Task Given_AnInvalidCommandHandler_When_CommandIsExecuted_Should_ReturnMessage()
        {
            // Arrange
            Client client = new("Carlos", "@carlos");
            UpdateClientCommand command = new()
            {
                ClientId = 1,
                NewName = "Pablo",
                NewContact = "@szpbl"
            };

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            client.Name.Should().Be("Carlos");
            client.Contact.Should().Be("@carlos");
            _clientRepositoryMock.Verify(x => x.UpdateAsync(client), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == "Cliente com o Id 1 não encontrado.");
        }    

        [Fact]
        public async Task Given_AnEmptyNewNameCommand_When_CommandIsExecuted_Should_UpdateOnlyContact()
        {
            // Arrange
            Client client = new("Carlos", "@carlos");
            UpdateClientCommand command = new()
            {
                ClientId = 1,
                NewContact = "@szpbl"
            };

            _clientRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(client);
                
            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            client.Name.Should().Be("Carlos");
            client.Contact.Should().Be("@szpbl");
            _clientRepositoryMock.Verify(x => x.UpdateAsync(client), Times.Once);
        }               
    }
}