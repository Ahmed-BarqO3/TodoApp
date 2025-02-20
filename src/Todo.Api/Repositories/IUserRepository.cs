namespace Todo.Api.Repositories;

public interface IUserRepository
{
    Task<bool> AddAsync(User? user);
    Task<User?> GetByEmail(string email);
    Task<bool> IsEmailExist(string email);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(string  Id);
    Task<User?> FindbyId(string id);
}
