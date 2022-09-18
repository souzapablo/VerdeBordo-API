using VerdeBordo.Core.Entities.Base;

namespace VerdeBordo.Core.Entities
{
    public class Embroidery : BaseEntity
    {
        
        #region Constructors

        public Embroidery(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        #endregion

        #region Properties

        public int OrderId { get; set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        #endregion
    }
}