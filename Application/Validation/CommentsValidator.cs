using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FluentValidation;

namespace Application.Validation;
public class CommentsValidator : AbstractValidator<Comments>
{
    public CommentsValidator()
    {
        RuleFor(x => x.Content).NotEmpty();
    }
}
