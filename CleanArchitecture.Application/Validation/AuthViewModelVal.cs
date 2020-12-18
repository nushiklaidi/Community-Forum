using CleanArchitecture.Application.ViewModels;
using FluentValidation;

namespace CleanArchitecture.Application.Validation
{
    public class AuthViewModelVal : AbstractValidator<AuthViewModel>
    {
        public AuthViewModelVal()
        {
            RuleFor(a => a.Email)
                .NotEmpty();
            RuleFor(a => a.Password)
                .NotEmpty();
        }
    }
}
