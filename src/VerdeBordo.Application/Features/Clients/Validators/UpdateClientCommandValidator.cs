using FluentValidation;
using VerdeBordo.Application.Features.Clients.Commands.UpdateClient;

namespace VerdeBordo.Application.Features.Clients.Validators
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.NewName)
                .MinimumLength(3)
                .WithMessage("O nome deve conter no mínimo 3 caracteres.")
                .MaximumLength(255)
                .WithMessage("O nome deve conter no máximo 255 caracteres.");
            
            RuleFor(x => x.NewContact)
                .MinimumLength(3)
                .WithMessage("O contato deve conter no mínimo 3 caracteres.")
                .MaximumLength(255)
                .WithMessage("O contato deve conter no máximo 255 caracteres.");
        }        
    }
}