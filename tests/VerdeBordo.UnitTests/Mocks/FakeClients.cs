using Bogus;

namespace VerdeBordo.UnitTests.Mocks
{
    public class FakeClient : Faker<Client>
    {
        public FakeClient() : base("pt_BR")
        {
            RuleFor(c => c.Id, c => c.IndexFaker + 1)
                .RuleFor(c => c.CreatedAt, c => c.Date.Recent(5))
                .RuleFor(c => c.Name, c => c.Name.FirstName())
                .RuleFor(c => c.Contact, c => c.Phone.Locale);
        }
    }
}