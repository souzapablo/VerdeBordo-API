using FluentValidation;
using VerdeBordo.Application.Features.Orders.Commands.AddDeliveryFeeToOrder;

namespace VerdeBordo.Application.Features.Orders.Validators
{   
    public class AddDeliveryFeeToOrderCommandValidator : AbstractValidator<AddDeliveryFeeToOrderCommand>
    {
        public AddDeliveryFeeToOrderCommandValidator()
        {
            RuleFor(x => x.DeliveryFee)
                .GreaterThan(0)
                .WithMessage("Valor da taxa de entrega deve ser maior do que 0.");
        }
    }
}