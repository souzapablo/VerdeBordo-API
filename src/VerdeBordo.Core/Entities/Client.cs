using VerdeBordo.Core.Entities.Base;

namespace VerdeBordo.Core.Entities
{
    public class Client : BaseEntity
    {

        #region Constructors

        public Client()
        { 
            Orders = new();
        }

        public Client(string name, string contact) 
        {
            Name = name;
            Contact = contact;

            Orders = new();
        }

        #endregion

        #region Properties

        public string Name { get; private set; } = null!;
        public string Contact { get; private set; } = null!;
        public List<Order> Orders { get; private set; }

        #endregion
    }
}