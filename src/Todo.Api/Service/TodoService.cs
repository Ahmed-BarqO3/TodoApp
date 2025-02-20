using Mapster;
using Todo.Api.Repositories;
using Todo.Api.Response;

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


    public async Task<TodoResponse?> GetAsync(int id)
    {
       var result = await  _todoRepository.GetByIdAsync(id);
       return result.Adapt<TodoResponse>();
    }


    public async Task<List<TodoResponse>> GetAllAsync(string userid)
    {
       var restult = await  _todoRepository.GetAllAsync(userid);
       return restult.Adapt<List<TodoResponse>>();
    }

    public async Task<bool> UpdateAsync(Models.Todo todo) =>

        await _todoRepository.UpdateAsync(todo);
    

    public async Task<bool> DeleteAsync(int id) =>
    
        await _todoRepository.DeleteAsync(id);


    public async Task<bool> DoneAsync(int id) =>
 
        await _todoRepository.DoneAsync(id) ? true : false;   
    
}
