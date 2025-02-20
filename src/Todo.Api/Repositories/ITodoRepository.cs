
namespace Todo.Api.Repositories;

public interface ITodoRepository
{
    Task<Models.Todo> AddAsync(Models.Todo? todo);
    Task<Models.Todo?> GetByIdAsync(int id);
    Task<List<Models.Todo>> GetAllAsync(string userid);
    Task<bool> UpdateAsync(Models.Todo todo);
    Task<bool> DeleteAsync(int id);

    Task<bool> DoneAsync(int id);



}
