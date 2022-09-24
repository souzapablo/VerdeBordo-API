using VerdeBordo.Application.Features.Clients.Commands.DeleteClient;

namespace VerdeBordo.UnitTests.Features.Clients.Commands
{
    public class DeleteClientCommandHandlerTests
    {
        private readonly DeleteClientCommandHandler _commandHandler;
        private readonly Mock<IClientRepository> _clientRepoistoryMock = new();
        private readonly Mock<MessageHandler> _messageHandlerMock = new();
        
        public DeleteClientCommandHandlerTests()
        {
            _commandHandler = new(_clientRepoistoryMock.Object, _messageHandlerMock.Object);
        }

        [Fact]
        public async Task Given_AValidClientId_When_CommandIsExecuted_Should_SetIsDeletedTrue()
        {
            // Arrange
            Client client = new("Nelson", "@nelson");
            DeleteClientCommand command = new(1);

            _clientRepoistoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(client);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            client.IsDeleted.Should().BeTrue();
            _clientRepoistoryMock.Verify(x => x.UpdateAsync(client), Times.Once);
        }

        [Fact]
        public async Task Given_AnInvalidClientId_When_CommandIsExecuted_Should_ReturnMessage()
        {
            // Arrange
            DeleteClientCommand command = new(1);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _clientRepoistoryMock.Verify(x => x.UpdateAsync(It.IsAny<Client>()), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == "Cliente com o Id 1 não encontrado.");
        }    

        [Fact]
        public async Task Given_ADeletedClient_When_CommandIsExecuted_Should_ReturnMessage()
        {
            // Arrange
            Client client = new("Nelson", "@nelson");
            DeleteClientCommand command = new(1);
            client.SetIsDeleted(true);

            _clientRepoistoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(client);

            // Act
            await _commandHandler.Handle(command, new CancellationToken());

            // Assert
            _clientRepoistoryMock.Verify(x => x.UpdateAsync(It.IsAny<Client>()), Times.Never);
            _messageHandlerMock.Object.Messages.Should().Contain(x => x.Value == "Cliente já foi apagado anteriormente.");
        }      
    }
}