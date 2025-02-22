using FluentValidation;

namespace Todo.Api.Filters;

public class ValidationFilter<TRequset> : IEndpointFilter
{
    readonly IValidator<TRequset> _validator;

    public ValidationFilter(IValidator<TRequset> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
         var requset = context.Arguments.OfType<TRequset>().First();

         var result = await _validator.ValidateAsync(requset, context.HttpContext.RequestAborted);

         if (!result.IsValid)
         {
             return TypedResults.ValidationProblem(result.ToDictionary());
         }
       return  await next(context);
    }
}


