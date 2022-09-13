namespace VerdeBordo.UnitTests.Entities
{
    public class OrderTests
    {
        [Fact]
        public void Given_AValidEmbroidery_When_AddedToOrderEmbroideriesList_Should_UpdateOrderPrice()
        {
            // Arange
            Client client = new Client("Cliente", "@cliente");
            Order order = new(DateTime.Now, client, PaymentMethod.BankTransfer, false);
            Embroidery embroidery = new("Novo bordado", 2_25);

            // Act
            order.AddEmbroidery(embroidery);

            // Assert
            order.Embroideries.Should().HaveCount(1);
            order.OrderPrice.Equals(embroidery.Price);
        }

        [Fact]
        public void Given_AValidPayment_When_AddedToOrderPaymentList_Should_IncreasePayedAmount()
        {
            // Arange
            Client client = new Client("Cliente", "@cliente");
            Order order = new(DateTime.Now, client, PaymentMethod.BankTransfer, false);
            Payment payment = new(DateTime.Now, 2_25);

            // Act
            order.AddPayment(payment);

            // Assert
            order.Payments.Should().HaveCount(1);
            order.PayedAmount.Should().Be(payment.PaymentValue);
        }

        [Fact]
        public void Given_NotNullDeliveryFee_When_DeliveryFeeIsAdded_Should_IncreaseOrderPrive()
        {
            // Arrange
            Client client = new Client("Cliente", "@cliente");
            Order order = new(DateTime.Now, client, PaymentMethod.BankTransfer, false);
            Payment payment = new(DateTime.Now, 2_25);
            Embroidery embroidery = new("Novo bordado", 2_25);
            order.AddEmbroidery(embroidery);

            // Act
            order.SetDeliveryFee(2_50);

            // Assert
            order.DeliveryFee.Should().Be(2_50);
            order.OrderPrice.Should().Be(4_75);
        }

        [Fact]
        public void Given_AValidStatus_When_DeliveryIsRecorded_Should_ChangeStatus()
        {
            // Arrange
            Client client = new Client("Cliente", "@cliente");
            Order order = new(DateTime.Now, client, PaymentMethod.BankTransfer, false);
            order.SetStatus(OrderStatus.Delivering);

            // Act
            order.DeliverOrder(new DateTime(2022, 9, 12));

            // Assert
            order.OrderStatus.Should().Be(OrderStatus.Delivered);
            order.DeliveredAt.Should().Be(new DateTime(2022, 9, 12));
        }

        [Fact]
        public void Given_AnInvalidStatus_When_DeliveryIsRecorded_Should_ThrowInvalidStatusExceptions()
        {
            // Arrange
            Client client = new Client("Cliente", "@cliente");
            Order order = new(DateTime.Now, client, PaymentMethod.BankTransfer, false);

            // Act
            Action action = () => order.DeliverOrder(new DateTime(2022, 9, 12));

            // Assert
            action.Should().Throw<InvalidStatusException>()
                .WithMessage($"Status {order.OrderStatus} inválido.");
        }        
    }
}