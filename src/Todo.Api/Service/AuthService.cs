using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Todo.Api.Mapping;
using Todo.Api.Repositories;
using Todo.Api.Requsets;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Todo.Api.Service;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        this.config = config;
    }

    public async Task<UserDto?> RegisterAsync(CreateRegistreationRequset request)
    {
        if (await _userRepository.IsEmailExist(request.Email))
            return null;

        var user = new User();
        var hashPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

        user = request.Adapt<User>();
        user.Password = hashPassword;

        await _userRepository.AddAsync(user);
        return user.Adapt<UserDto>();

    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user is null)
            return null;

        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var key = config["Jwt:Key"];


        var calims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Name, user.FirstName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };
        var creadintials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"],
            Subject = new ClaimsIdentity(calims),
            Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:AccessExpiration")),
            SigningCredentials = creadintials
        };
        
        return tokenHandler.CreateToken(tokenDescriptor);
    }
}
