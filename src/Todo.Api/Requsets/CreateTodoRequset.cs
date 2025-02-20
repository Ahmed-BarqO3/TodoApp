using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Todo.Api.Requsets;

public record CreateTodoRequset
{
    public string Title { get; set; } = string.Empty;
}

public class CreateTodoRequsetValidator : AbstractValidator<CreateTodoRequset>
{
    public CreateTodoRequsetValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(70);
    }
}
