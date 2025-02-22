using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Api.Extensions;
using Todo.Api.Requsets;
using Todo.Api.Response;
using Todo.Api.Service;

namespace Todo.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/user");
            

        group.MapPost("register", RegisterAsync)
            .WithRequsetValidation<CreateRegistreationRequset>();

        group.MapPost("logout", LogoutAsync)
          .RequireAuthorization();

        group.MapGet("me", GetCurrentUser)
          .RequireAuthorization();

        group.MapPut("update", UpdateAsync)
          .WithRequsetValidation<UpdateUserRequest>();


    }

    private static async Task<Results<Ok<UserResponse>,BadRequest>> UpdateAsync(ClaimsPrincipal claims,UpdateUserRequest request,IUserService userService)
    {
        var userid = claims.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var result = await userService.UpdateAsync(userid, request);
        

        return result is not null ? TypedResults.Ok(result.Adapt<UserResponse>()): TypedResults.BadRequest();

    }
    
    private static async Task<Results<Ok,BadRequest>> RegisterAsync(CreateRegistreationRequset request, IUserService userService)
    {
        var user = await userService.RegisterAsync(request);
        if (user is false)
            return TypedResults.BadRequest();
        
        return TypedResults.Ok();
    }

    private static async Task<IResult> LogoutAsync(IUserService userService)
    {
        
           await userService.LogoutAsync();
           return Results.Ok();
    }

    private static async Task<Results<Ok<UserResponse>,BadRequest>> GetCurrentUser(ClaimsPrincipal claims,IUserService userService)
    {
        var userid =  claims.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var user = await userService.GetCurrentUserAsync(userid);

        return user is not null ? TypedResults.Ok(user) : TypedResults.BadRequest();

    }
}
