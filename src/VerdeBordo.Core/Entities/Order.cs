using VerdeBordo.Core.Entities.Base;
using VerdeBordo.Core.Enums;
using VerdeBordo.Core.Exceptions;

namespace VerdeBordo.Core.Entities
{

    public class Order : BaseEntity
    {

        #region Constructors

        public Order() 
        {
            Embroideries = new();
            Payments = new();
        }
        
        public Order(DateTime orderDate, int clientId, PaymentMethod paymentMethod, bool promptDelivery, decimal? deliveryFee = null)
        {
            OrderDate = orderDate;
            ClientId = clientId;
            PaymentMethod = paymentMethod;
            PromptDelivery = promptDelivery;
            DeliveryFee = deliveryFee;
            OrderStatus = OrderStatus.Created;

            Embroideries = new();
            Payments = new();
        }

        #endregion

        #region Properties

        public DateTime OrderDate { get; private set; }
        public int ClientId { get; private set; }
        public Client? Client { get; private set; }
        public List<Embroidery> Embroideries { get; private set; }
        public decimal OrderPrice { get; private set; }
        public decimal? DeliveryFee { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public List<Payment> Payments { get; private set; }
        public bool PromptDelivery { get; private set; }
        public decimal PayedAmount { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime? DeliveredAt { get; private set; }

        #endregion

        #region Methods

        public void AddEmbroidery(Embroidery embroidery)
        {
            Embroideries.Add(embroidery);
            OrderPrice += embroidery.Price;
        }

        public void AddPayment(Payment payment)
        {
            Payments.Add(payment);
            PayedAmount += payment.PaymentValue;
        }

        public void SetDeliveryFee(decimal deliveryFee)
        {
            DeliveryFee = deliveryFee;
            OrderPrice += deliveryFee;
        }

        public void SetStatus(OrderStatus newStatus) => OrderStatus = newStatus;

        public void DeliverOrder(DateTime delivereAt)
        {
            DeliveredAt = delivereAt;
            SetStatus(OrderStatus.Delivered);
        }

        #endregion

    }
}