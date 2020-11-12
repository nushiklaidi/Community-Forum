using CleanArchitecture.Application.ViewModels;
using FluentValidation;

namespace CleanArchitecture.Application.Validation
{
    public class AuthViewModelVal : AbstractValidator<AuthViewModel>
    {
        public AuthViewModelVal()
        {
            RuleFor(a => a.Username)
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .NotNull();
            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .NotNull();
        }
    }
}
