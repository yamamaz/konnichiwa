using Application.Interface;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

public class UserValidator : AbstractValidator<User>
{
    private readonly IKonnichiwaDbContext _context;

    public UserValidator(IKonnichiwaDbContext context)
    {
        RuleFor(x => x.Name).MinimumLength(1).MaximumLength(30);
        RuleFor(x => x.Email).EmailAddress();
        _context = context;
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(6)
            .Must(username => IsUsernameUnique(username))
            .WithMessage("Username is already taken");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
        RuleFor(x => x.Bio).MaximumLength(10000);
    }

    private bool IsUsernameUnique(string username)
    {
        return _context.Users.FirstOrDefault(x => x.UserName == username) == null;
    }

    public IEnumerable<ValidationFailure> ValidateWithErrors(User user)
    {
        var result = Validate(user);
        return result.Errors;
    }
}
