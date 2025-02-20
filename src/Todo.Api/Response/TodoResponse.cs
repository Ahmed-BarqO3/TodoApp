namespace Todo.Api.Response;

public class TodoResponse
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public bool IsComplete { get; set; }
    public  DateTime CreateAt { get;  set; }
}
