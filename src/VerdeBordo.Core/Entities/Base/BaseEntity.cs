namespace VerdeBordo.Core.Entities.Base
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
        public bool IsDeleted { get; private set; }

        #endregion

        #region Methods

        public void SetIsDeleted(bool isDeleted) => IsDeleted = isDeleted;

        #endregion
    }
}