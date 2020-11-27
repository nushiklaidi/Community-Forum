using CleanArchitecture.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Validation
{
    public class UserViewModelVal : AbstractValidator<UserViewModel>
    {
        public UserViewModelVal()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty")
                .NotNull();            
        }
    }
}
