namespace Backend.Models;

public class Board
{
    public long Id { get; set; }
    public string Name { get; set; } = "Example Board";
    public ICollection<BoardList> BoardLists { get; } = new List<BoardList>();
}