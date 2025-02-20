using Todo.Api.Response;

namespace Todo.Api.Service;

public interface ITodoService
{
    Task<Models.Todo> AddAsync(Models.Todo todo);
    Task<TodoResponse?> GetAsync(int id);
    Task<List<TodoResponse>> GetAllAsync(string userid);
    Task<bool> UpdateAsync(Models.Todo todo);
    Task<bool> DoneAsync(int id);
    Task<bool> DeleteAsync(int id);
}
