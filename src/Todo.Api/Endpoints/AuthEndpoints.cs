using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Api.Filters;
using Todo.Api.Mapping;
using Todo.Api.Requsets;
using Todo.Api.Service;

namespace Todo.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/auth");
            

        group.MapPost("register", RegisterAsync).WithRequsetValidation<CreateRegistreationRequset>();
        group.MapPost("login", LoginAsync);
    }
    
    
    private static async Task<Results<Ok<UserDto>,BadRequest<string>>> RegisterAsync(CreateRegistreationRequset request, IAuthService authService)
    {
        var user = await authService.RegisterAsync(request);
        if (user is null)
            return TypedResults.BadRequest("Email already exist");
        
        return TypedResults.Ok(user);
    }
    
    private static async Task<Results<Ok<string>,BadRequest<string>>> LoginAsync(string email, string password, IAuthService authService)
    {
        var token = await authService.LoginAsync(email, password);
        if (token is null)
            return TypedResults.BadRequest("Invalid email or password");
        
        return TypedResults.Ok(token);
    }
   
}
