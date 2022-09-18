using FluentValidation;
using VerdeBordo.Application.Features.Orders.Commands.AddEmbroideryToOrder;

namespace VerdeBordo.Application.Features.Orders.Validators
{
    public class AddEmbroideryToOrderCommandValidator : AbstractValidator<AddEmbroideryToOrderCommand>
    {
        public AddEmbroideryToOrderCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("A descrição do bordado deve ser informada.")
                .NotNull()
                .WithMessage("A descrição do bordado não pode ser nula.");
            
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("O valor do bordado deve ser informado.");
        }
    }
}