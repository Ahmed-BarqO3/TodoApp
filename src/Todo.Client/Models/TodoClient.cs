using System.Net.Http.Json;

namespace Todo.Client.Models;

public class TodoClient(IHttpClientFactory httpClientFactory)
{
   readonly HttpClient httpClient = httpClientFactory.CreateClient("Auth");

    public async Task<TodoItem?> AddTodoAsync(string title)
    {
        if(string.IsNullOrWhiteSpace(title))
            return null;
        
        TodoItem? todoItem = null;
        
        var response = await httpClient.PostAsJsonAsync("api/v1/todo", new { Title = title });
        
        if(response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TodoItem>();

        return todoItem;
    }
    
    public async Task<bool> DeleteTodoAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"api/v1/todo/{id}");
        return response.IsSuccessStatusCode;
    }
    
    public async Task<List<TodoItem>> GetTodosAsync()
    {
        var response = await httpClient.GetAsync("api/v1/todo");
        if(response.IsSuccessStatusCode)
        return await response.Content.ReadFromJsonAsync<List<TodoItem>>();

        return null;
    }
    
    public async Task<bool> DoneTodoAsync(TodoItem todoItem)
    {
        var response = await httpClient.PutAsync($"api/v1/todo/{todoItem.Id}/done", content:null);
        return response.IsSuccessStatusCode;
    }
    
}
