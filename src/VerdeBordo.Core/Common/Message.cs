namespace VerdeBordo.Core.Common
{
    public class Message
    {
        public Message(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}
