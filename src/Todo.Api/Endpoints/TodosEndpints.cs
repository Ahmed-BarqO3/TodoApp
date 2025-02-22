using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Api.Extensions;
using Todo.Api.Filters;
using Todo.Api.Requsets;
using Todo.Api.Response;
using Todo.Api.Service;

namespace Todo.Api.Endpoints;

public static class TodosEndpints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app )
    {
        var group = app.MapGroup("api/v1/todo")
            .RequireAuthorization();

        group.MapPost("", CreateTodo)
            .WithRequsetValidation<CreateTodoRequset>();
        
        group.MapGet("{id}", GetTodo); 
        group.MapDelete("{id}", DeleteTodo);
        group.MapGet("", GetTodos);
        group.MapPut("{id}/done", TodoDone);
        
    }
    
     static async Task<Results<Ok<TodoResponse>,BadRequest>> CreateTodo(ClaimsPrincipal claims,CreateTodoRequset requset ,ITodoService _todoService)
    {
        
        var userid = claims.FindFirst(x=>x.Type == ClaimTypes.NameIdentifier).Value;
        var itme = requset.Adapt<Models.Todo>();
        itme.UserId = userid;
       
        var result = await _todoService.AddAsync(itme);
        return result  is not null? TypedResults.Ok(result.Adapt<TodoResponse>()) : TypedResults.BadRequest();
    }
    
     static async Task<Results<Ok<TodoResponse>,NotFound>> GetTodo( int id,ITodoService todoService)
    {
        var result = await todoService.GetAsync(id);
        return result is not null? TypedResults.Ok(result) : TypedResults.NotFound();
    }
    
     static async Task<Results<NoContent,NotFound>> DeleteTodo( int id,ITodoService todoService)
    {
        var result = await todoService.DeleteAsync(id);
        return result ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    static async Task<Results<Ok, BadRequest>> TodoDone(int id, ITodoService todoService)
    {
        var result = await todoService.DoneAsync(id);
        return result ? TypedResults.Ok() : TypedResults.BadRequest();
    }
    
     static async Task<IResult> GetTodos(ClaimsPrincipal calims,ITodoService todoService)
    {
        var userid = calims.FindFirst(x=>x.Type == ClaimTypes.NameIdentifier).Value;
        
        var result = await todoService.GetAllAsync(userid);
        return TypedResults.Ok(result);
    }
}
