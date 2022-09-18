using FluentValidation;
using VerdeBordo.Application.Features.Orders.Commands.AddPaymentToOrder;
using VerdeBordo.Core.Interfaces.Repositories;

namespace VerdeBordo.Application.Features.Orders.Validators
{
    public class AddPaymentToOrderCommandValidator : AbstractValidator<AddPaymentToOrderCommand>
    {

        public AddPaymentToOrderCommandValidator()
        {  
            RuleFor(x => x.PayedAmount)
                .GreaterThan(0)
                .WithMessage("O valor do pagamento deve ser informado.");

            RuleFor(x => x.PaymentDate)
                .NotEmpty()
                .WithMessage("A data do pagamento deve ser informada");
        }
    }
}