using Todo.Api.Requsets;
using Todo.Api.Response;

namespace Todo.Api.Service;

public interface IUserService
{
    Task<bool> RegisterAsync(CreateRegistreationRequset register);
    Task LogoutAsync();

    Task<UserResponse> GetCurrentUserAsync(string userid);

    Task<User?> UpdateAsync(string userid,UpdateUserRequest request);
}
