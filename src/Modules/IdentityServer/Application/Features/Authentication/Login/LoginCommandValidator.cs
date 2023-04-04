using FluentValidation;

namespace Ark.IdentityServer.Application.Features.Authentication.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Incorrect phone number");
        // RuleFor(x => x.Login).NotEmpty().WithMessage("Username is required");
        // RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}