namespace VerdeBordo.Core.Entities
{
    public class BaseEntity
    {

        #region Constructors

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        #endregion
    }
}