using System.Net.Http.Json;
using System.Text.Json;

namespace Todo.Client.Models;

public class UserClient(IHttpClientFactory httpClientFactory)
{
    readonly HttpClient httpClient = httpClientFactory.CreateClient("Auth");


    public async Task<UserInfo?> GetUserAsync()
    {
        var response = await httpClient.GetAsync("api/v1/user/me");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<UserInfo>();
        return null;
    }

    public async Task<UserInfo?> UpdateUserAsync(UpdateUserModel userInfo)
    {
        var response = await httpClient.PutAsJsonAsync("api/v1/user/update", userInfo);
        if (response.IsSuccessStatusCode)
        {
            var userJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserInfo>
                (userJson, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        }
        return null;
    }
}
