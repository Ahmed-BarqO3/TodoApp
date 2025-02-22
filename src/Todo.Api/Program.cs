using Todo.Api.Data;
using Todo.Api.Endpoints;
using Todo.Api.Repositories;
using Todo.Api.Service;
using FluentValidation;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Todo.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        o =>
        {
            o.AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithOrigins(builder.Configuration.GetValue<string>("WebHost"))
                   .AllowCredentials();
        }); 
});


builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    //app.ApplyMigrations();

}

app.UseHttpsRedirection();

app.UseExceptionHandler(app =>
{
    app.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature is not null)
        {
            await context.Response.WriteAsJsonAsync(new 
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            });
        }
    });
});


app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();

app.MapAuthEndpoints();
app.MapTodoEndpoints();

app.Run();
