using Todo.Api.Mapping;

namespace Todo.Api.Service;

public interface ITodoService
{
    Task<Models.Todo> AddAsync(Models.Todo todo);
    Task<TodoDto?> GetAsync(int id);
    Task<List<TodoDto>> GetAllAsync();
    Task<bool> UpdateAsync(Models.Todo todo);
    Task<bool> DoneAsync(int id);
    Task<bool> DeleteAsync(int id);
}
