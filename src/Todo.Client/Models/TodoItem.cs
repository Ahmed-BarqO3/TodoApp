using System.ComponentModel.DataAnnotations;

namespace Todo.Client.Models;

public class TodoItem
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public DateTime CreateAt { get; set; }
}
