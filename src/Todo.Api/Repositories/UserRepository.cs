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
            .SetProperty(x=>x.PasswordHash, user.PasswordHash)) > 0;
        
    
    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _context.Users.FindAsync( id);
        if (user is null)
            return false;
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<User?> FindbyId(string id) =>
   
        await _context.Users.FindAsync(id);
    
}
