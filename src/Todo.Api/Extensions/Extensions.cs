using Todo.Api.Data;
using Todo.Api.Filters;

namespace Todo.Api.Extensions;

public static class Extensions
{
    public static void WithRequsetValidation<TFilter>(this RouteHandlerBuilder builder)

        => builder.AddEndpointFilter<ValidationFilter<TFilter>>().ProducesValidationProblem();



    public static void ApplyMigrations(this IApplicationBuilder builder)
    {
        using IServiceScope scope = builder.ApplicationServices.CreateScope();
        using AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
    }
}


