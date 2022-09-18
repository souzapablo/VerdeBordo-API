using VerdeBordo.Core.Common;

namespace VerdeBordo.Core.Interfaces.Messages
{
    public interface IMessageHandler
    {
        List<Message> Messages { get; }
        bool HasMessage { get; }
        void AddMessage(string key, string message);
        void AddMessages(List<Message> messages);
    }
}
