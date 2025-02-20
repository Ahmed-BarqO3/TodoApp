using Microsoft.AspNetCore.Identity;

namespace Todo.Api.Models;

public class User() : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Todo>? Todo = new List<Todo>();
    
}
