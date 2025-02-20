namespace Todo.Client.Models;

public interface IAccountManagment
{
    Task<AuthResult> LoingAsync(LoginModel loginModel);
    Task<AuthResult> RegisterAsync(RegisterModel registerModel);
    Task LogoutAsync();
}
