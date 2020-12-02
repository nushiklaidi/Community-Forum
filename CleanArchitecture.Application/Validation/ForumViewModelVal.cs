using CleanArchitecture.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Validation
{
    public class ForumViewModelVal : AbstractValidator<ForumAddViewModel>
    {
        public ForumViewModelVal()
        {
            RuleFor(r => r.Title)
                .NotEmpty()
                .NotNull();
            RuleFor(r => r.Description)
                .NotEmpty()
                .NotNull();
        }
    }
}
