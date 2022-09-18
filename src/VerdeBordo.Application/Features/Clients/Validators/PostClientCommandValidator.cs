using FluentValidation;
using VerdeBordo.Application.Features.Clients.Commands.PostClient;

namespace VerdeBordo.Application.Features.Clients.Validators
{
    public class PostClientCommandValidator : AbstractValidator<PostClientCommand>
    {
        public PostClientCommandValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O nome do cliente é obrigatório.")
                .NotEmpty()
                .WithMessage("O nome do cliente deve ser informado.");
            
            RuleFor(x => x.Contact)
                .NotNull()
                .WithMessage("O contato do cliente é obrigatório.")
                .NotEmpty()
                .WithMessage("O contato do cliente deve ser informado.");
        }
    }
}