using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FluentValidation;

namespace Application.Validation;
public class PostValidator : AbstractValidator<Post>
{
    public PostValidator()
    {
        RuleFor(x=> x.Body).NotEmpty().MaximumLength(10000);
        RuleFor(x => x.Name).MaximumLength(100);
        RuleFor(x => x.Created).NotEmpty();
    }
}
