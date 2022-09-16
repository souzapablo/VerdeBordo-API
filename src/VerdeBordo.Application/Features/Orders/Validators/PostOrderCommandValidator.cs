using FluentValidation;
using VerdeBordo.Application.Features.Orders.Commands.PostOrderCommand;

namespace VerdeBordo.Application.Features.Orders.Validators
{
    public class PostOrderCommandValidator : AbstractValidator<PostOrderCommand>
    {
        public PostOrderCommandValidator()
        {

            RuleFor(x => x.ClientId)
                .GreaterThan(0)
                .WithMessage("Id do cliente inválido.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .WithMessage("Método de pagamento inválido.");

            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("A data do pedido deve ser informada.");

            RuleFor(x => x.IsPromptDelivery)
                .NotEmpty().WithMessage("É necessário informar se o pedido é pronta entrega.");

            When(x => x.DeliveryFee.HasValue, () =>
            {
                RuleFor(x => x.DeliveryFee)
                    .GreaterThan(0)
                    .WithMessage("Valor de taxa de entrega inválido.");
            });
        }
    }
}
