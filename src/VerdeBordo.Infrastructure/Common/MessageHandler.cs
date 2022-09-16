using VerdeBordo.Core.Common;
using VerdeBordo.Core.Interfaces.Messages;

namespace VerdeBordo.Infrastructure.Common
{
    public class MessageHandler : IMessageHandler
    {
        private readonly List<Message> _messages;

        public MessageHandler()
        {
            _messages = new();
        }

        public List<Message> Messages => _messages;

        public bool HasMessage => _messages.Any();

        public void AddMessage(string key, string message) => _messages.Add(new Message(key, message));

        public void AddMessages(List<Message> messages) => _messages.AddRange(messages);
    }
}
