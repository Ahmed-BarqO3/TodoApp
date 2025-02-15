using Todo.Api.Data;

namespace Todo.Api.Repositories;

public class UserRepository : IUserRepository
{
    AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(User? user)
    {
         await _context.Users.AddAsync(user);
         return _context.SaveChanges() > 0;
    }

    public async Task<User?> GetByEmail(string email) =>
    
        await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<bool> IsEmailExist(string email) =>
        await _context.Users.AsNoTracking().AnyAsync(x=> x.Email == email);
    

    public async Task<bool> UpdateAsync(User user) =>
    
          await _context.Users.ExecuteUpdateAsync(p=> p.SetProperty(x=>x.FirstName, user.FirstName)
            .SetProperty(x=>x.LastName, user.LastName)
            .SetProperty(x=>x.Email, user.Email)
            .SetProperty(x=>x.Password, user.Password)) > 0;
        
    
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync( id);
        if (user is null)
            return false;
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync() > 0;
    }
}
