namespace VerdeBordo.Core.Entities
{
    public class Client
    {

        #region Constructors

        public Client(string name, string contact)
        {
            Name = name;
            Contact = contact;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public string Contact { get; private set; }

        #endregion
    }
}