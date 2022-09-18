using FluentValidation;
using VerdeBordo.Application.Features.Orders.Commands.UpdateOrderStatus;
using VerdeBordo.Core.Enums;

namespace VerdeBordo.Application.Features.Orders.Validators
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.NewStatus)
                .IsInEnum()
                .WithMessage("Status inválido.");

            When(x => x.NewStatus == OrderStatus.Delivered, () => 
            {
                RuleFor(x => x.DeliveredAt)
                    .NotEmpty()
                    .WithMessage("A data deve ser preenchida quando o pedido for entregue.");
            });
        }
    }
}