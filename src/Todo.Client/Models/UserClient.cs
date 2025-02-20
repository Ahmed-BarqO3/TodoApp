using System.Net.Http.Json;

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
}
