using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Api.Filters;
using Todo.Api.Mapping;
using Todo.Api.Requsets;
using Todo.Api.Service;

namespace Todo.Api.Endpoints;

public static class TodosEndpints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app )
    {
        var group = app.MapGroup("api/v1/todos")
            .RequireAuthorization();

        group.MapPost("", CreateTodo)
            .WithRequsetValidation<CreateTodoRequset>();
        
        group.MapGet("{id}", GetTodo); 
        group.MapDelete("{id}", DeleteTodo);
        group.MapGet("", GetTodos);
        group.MapPut("{id}/done", TodoDone);
        
    }
    
     static async Task<Results<Ok<TodoDto>,BadRequest>> CreateTodo(CreateTodoRequset requset ,ITodoService _todoService)
    {
        var result = await _todoService.AddAsync(requset.Adapt<Models.Todo>());
        return result  is not null? TypedResults.Ok(result.Adapt<TodoDto>()) : TypedResults.BadRequest();
    }
    
     static async Task<Results<Ok<TodoDto>,NotFound>> GetTodo( int id,ITodoService todoService)
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
    
     static async Task<IResult> GetTodos(ITodoService todoService)
    {
        var result = await todoService.GetAllAsync();
        return TypedResults.Ok(result);
    }
}
