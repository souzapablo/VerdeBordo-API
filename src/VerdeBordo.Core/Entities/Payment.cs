using VerdeBordo.Core.Entities.Base;

namespace VerdeBordo.Core.Entities
{
    public class Payment : BaseEntity
    {
        public Payment(DateTime paymentDate, decimal paymentValue, int orderId)
        {
            OrderId = orderId;
            PaymentDate = paymentDate;
            PaymentValue = paymentValue;
        }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; private set; }
        public decimal PaymentValue { get; private set; }
    }
}