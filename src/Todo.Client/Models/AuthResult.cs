namespace Todo.Client.Models;


public class AuthResult
{
    public bool Succeede { get; set; }
    public string[] ErrorList { get; set; } = [];
}
