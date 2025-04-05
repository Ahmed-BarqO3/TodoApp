using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Todo.Client;
using Todo.Client.Identity;
using Todo.Client.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddTransient<CookieHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("Auth", client =>
    client.BaseAddress = new Uri("https://todo-api-gqbrhchqbecthhca.centralus-01.azurewebsites.net")
)
.AddHttpMessageHandler<CookieHandler>();


builder.Services.AddScoped<TodoClient>();
builder.Services.AddScoped<UserClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagment)sp.GetRequiredService<AuthenticationStateProvider>());

await builder.Build().RunAsync();
