using Todo.Api.Mapping;
using Todo.Api.Requsets;

namespace Todo.Api.Service;

public interface IAuthService
{
    Task<UserDto?> RegisterAsync(CreateRegistreationRequset register);
    Task<string?> LoginAsync(string email, string password);
}
