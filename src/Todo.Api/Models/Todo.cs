using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Models;
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public  DateTime CreateAt { get; private set; } = DateTime.Now;
    public User User { get; set; }
    public int UserId { get; set; }
}



