using Todo.Api.Data;

namespace Todo.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Models.Todo?> AddAsync(Models.Todo todo)
    {
        await _context.Todos.AddAsync(todo);
        return _context.SaveChanges() > 0 ? todo: null;
    }

    public async Task<Models.Todo?> GetByIdAsync(int id) =>
    
        await _context.Todos.FindAsync(id);
    

    public async Task<List<Models.Todo>> GetAllAsync(string userid) =>
        await _context.Todos.AsNoTracking().Where(x=>x.UserId == userid).ToListAsync();
    

    public async Task<bool> UpdateAsync(Models.Todo todo) =>
    
       await  _context.Todos.ExecuteUpdateAsync(x => x.SetProperty(t => t.Title, todo.Title)
            .SetProperty(t => t.IsComplete, todo.IsComplete)) > 0;

    public async Task<bool> DoneAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        todo.IsComplete = true;
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        
        var  todo = await _context.Todos.FindAsync(id);
        if (todo is null)
            return false;
        _context.Todos.Remove(todo);
        return await _context.SaveChangesAsync() > 0;
    }
}
