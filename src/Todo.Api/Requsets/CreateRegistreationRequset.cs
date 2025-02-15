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
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}
