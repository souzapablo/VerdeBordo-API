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
                .MinimumLength(3)
                .WithMessage("O nome deve conter no mínimo 3 caracteres.")
                .MaximumLength(255)
                .WithMessage("O nome deve conter no máximo 255 caracteres.")
                .NotNull()
                .WithMessage("O nome do cliente é obrigatório.")
                .NotEmpty()
                .WithMessage("O nome do cliente deve ser informado.");
            
            RuleFor(x => x.Contact)
                .MaximumLength(3)
                .WithMessage("O contato deve conter no mínimo 3 caracteres.")
                .MaximumLength(255)
                .WithMessage("O contato deve conter no máximo 255 caracteres.")
                .NotNull()
                .WithMessage("O contato do cliente é obrigatório.")
                .NotEmpty()
                .WithMessage("O contato do cliente deve ser informado.");
        }
    }
}