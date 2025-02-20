using Mapster;
using Microsoft.AspNetCore.Identity;
using Todo.Api.Repositories;
using Todo.Api.Requsets;
using Todo.Api.Response;

namespace Todo.Api.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    readonly UserManager<User> _userManager;
    readonly SignInManager<User> _signInManager;


    public UserService(IUserRepository userRepository,  UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<UserResponse> GetCurrentUserAsync(string userid)
    {

       var user = await _userRepository.FindbyId(userid);
        return user.Adapt<UserResponse>();
    }
    

    public async Task LogoutAsync() =>
    
       await _signInManager.SignOutAsync();
    

    public async Task<bool> RegisterAsync(CreateRegistreationRequset request)
    {
        if (await _userRepository.IsEmailExist(request.Email))
            return false;

        var user = new User();
      
        user = request.Adapt<User>();
        user.UserName = request.Email;

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user,true);
            return true;
        }
        return false;
    }

}
