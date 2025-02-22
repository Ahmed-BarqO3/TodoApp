using FluentValidation;

namespace Todo.Api.Requsets;

public record UpdateUserRequest(string FirstName, string LastName, string PasswordHash);



public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(x => x.PasswordHash)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.")
            .When(x => !string.IsNullOrEmpty(x.PasswordHash));

    }
}
