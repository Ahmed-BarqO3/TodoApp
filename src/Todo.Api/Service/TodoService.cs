using Mapster;
using Todo.Api.Mapping;
using Todo.Api.Repositories;

namespace Todo.Api.Service;

public class TodoService : ITodoService
{
    private ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
    
    public async Task<Models.Todo> AddAsync(Models.Todo todo) =>

        await _todoRepository.AddAsync(todo);


    public async Task<TodoDto?> GetAsync(int id)
    {
       var result = await  _todoRepository.GetByIdAsync(id);
       return result.Adapt<TodoDto>();
    }


    public async Task<List<TodoDto>> GetAllAsync()
    {
       var restult = await  _todoRepository.GetAllAsync();
       return restult.Adapt<List<TodoDto>>();
    }

    public async Task<bool> UpdateAsync(Models.Todo todo) =>

        await _todoRepository.UpdateAsync(todo);
    

    public async Task<bool> DeleteAsync(int id) =>
    
        await _todoRepository.DeleteAsync(id);


    public async Task<bool> DoneAsync(int id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        if (todo is null)
        {
            return false;
        }
        
        todo.IsComplete = true;
        return await _todoRepository.UpdateAsync(todo);
    }
    
}
