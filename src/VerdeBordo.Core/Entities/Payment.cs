namespace VerdeBordo.Core.Entities
{
    public class Payment : BaseEntity
    {
        public Payment(DateTime paymentDate, decimal paymentValue)
        {
            PaymentDate = paymentDate;
            PaymentValue = paymentValue;
        }

        public DateTime PaymentDate { get; private set; }
        public decimal PaymentValue { get; private set; }
    }
}