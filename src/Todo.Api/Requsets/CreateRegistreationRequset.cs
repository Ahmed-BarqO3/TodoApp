using FluentValidation;

namespace Todo.Api.Requsets;

public record CreateRegistreationRequset(string FirstName, string LastName, string Email, string Password);

public class CreateRegistreationRequsetValidator : AbstractValidator<CreateRegistreationRequset>
{
    public CreateRegistreationRequsetValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
    }
}
