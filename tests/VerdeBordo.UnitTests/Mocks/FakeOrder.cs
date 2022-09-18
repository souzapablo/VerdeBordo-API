using Bogus;

namespace VerdeBordo.UnitTests.Mocks
{
    public class FakeOrder : Faker<Order>
    {
        public FakeOrder()
        {
            RuleFor(o => o.Id, o => o.IndexFaker + 1)
                .RuleFor(o => o.CreatedAt, o => o.Date.Recent(5))
                .RuleFor(o => o.OrderDate, o => o.Date.Recent(1))
                .RuleFor(o => o.Client, new Client("Cliente", "@cliente"))
                .RuleFor(o => o.Embroideries, new List<Embroidery>())
                .RuleFor(o => o.OrderPrice, o => o.Finance.Amount(1, 1000, 2))
                .RuleFor(o => o.DeliveryFee, o => o.Finance.Amount(0, 15, 2))
                .RuleFor(o => o.PaymentMethod, o => o.Random.Enum<PaymentMethod>())
                .RuleFor(o => o.Payments, new List<Payment>())
                .RuleFor(o => o.PromptDelivery, o => o.Random.Bool())
                .RuleFor(o => o.PayedAmount, o => o.Finance.Amount(1, 1000, 2))
                .RuleFor(o => o.OrderStatus, o => o.Random.Enum<OrderStatus>());

        }
    }
}