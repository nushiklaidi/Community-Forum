using CleanArchitecture.Application.ViewModels;
using FluentValidation;

namespace CleanArchitecture.Application.Validation
{
    public class PostViewModelVal : AbstractValidator<PostViewModel>
    {
        public PostViewModelVal()
        {
            RuleFor(r => r.Title)
                .NotEmpty()
                .NotNull();
        }
    }
}
