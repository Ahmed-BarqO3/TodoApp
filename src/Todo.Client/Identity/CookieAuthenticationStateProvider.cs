using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Todo.Client.Models;
namespace Todo.Client;


public class CookieAuthenticationStateProvider(IHttpClientFactory httpClientFactory) : AuthenticationStateProvider, IAccountManagment
{
    bool _authenticated;
    readonly ClaimsPrincipal _unAuthenticated = new ClaimsPrincipal(new ClaimsIdentity());

    readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    readonly HttpClient _httpClient = httpClientFactory.CreateClient("Auth");

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _authenticated = false;

        var user = _unAuthenticated;

        try
        {
            var userResponse = await _httpClient.GetAsync("manage/info");
            userResponse.EnsureSuccessStatusCode();

            var userJson = await userResponse.Content.ReadAsStringAsync();
            var userInfo = JsonSerializer.Deserialize<UserInfo>(userJson, _jsonSerializerOptions);

            if (userInfo is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userInfo.Email),
                };

                var identity = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
                user = new ClaimsPrincipal(identity);
                _authenticated = true;
            }
        }
        catch
        {
            //logging
        }

        return new AuthenticationState(user);
    }

    public async Task<AuthResult> LoingAsync(LoginModel loginModel)
    {
        try
        {
            var Result = await _httpClient.PostAsJsonAsync("login?useCookies=true", new
            {
                loginModel.Email,
                loginModel.Password,
            });

            if (Result.IsSuccessStatusCode)
            {
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return new AuthResult { Succeede = true };
            }

        }
        catch
        {
            //logging
        }

        return new AuthResult
        {
            Succeede = false,
            ErrorList = ["Invalid email or password"]
        };
    }

    

    public async Task LogoutAsync()
    {
        await _httpClient.PostAsync("api/v1/user/logout", content: null);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<AuthResult> RegisterAsync(RegisterModel registerModel)
    {
        var errors = new List<string>();

        try
        {
            var Result = await _httpClient.PostAsJsonAsync("api/v1/user/register", new
            {
                registerModel.FirstName,
                registerModel.LastName,
                registerModel.Email,
                registerModel.Password,
            });
            if (Result.IsSuccessStatusCode)
            {
                return new AuthResult { Succeede = true };
            }

            var details = await Result.Content.ReadAsStringAsync();
            var errorList = JsonDocument.Parse(details).RootElement.GetProperty("errors");

            foreach (var error in errorList.EnumerateObject())
            {
                if (error.Value.ValueKind == JsonValueKind.String)
                {
                    errors.Add(error.Value.GetString());
                }
                else if (error.Value.ValueKind == JsonValueKind.Array)
                {
                    var allErrors = error.Value
                        .EnumerateArray()
                        .Select(e => e.GetString() ?? string.Empty)
                        .Where(e => !string.IsNullOrEmpty(e));

                    errors.AddRange(allErrors);
                }
            }

        }
        catch
        {
            //logging

        }
            return new AuthResult { Succeede = false, ErrorList = errors.ToArray() };
    }
}
