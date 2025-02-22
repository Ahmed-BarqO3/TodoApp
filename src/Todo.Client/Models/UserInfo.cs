using System.ComponentModel.DataAnnotations;

namespace Todo.Client.Models;

public class UserInfo
{
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed{ get; set; }

    public Dictionary<string, string> Claims { get; set; } = [];

    public string FullName
    {
        get => FirstName + " " + LastName;

    }

   
}
