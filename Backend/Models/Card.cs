namespace Backend.Models;

public class Card
{
    public long Id { get; set; }
    public string Title { get; set; } = "Example Card";
    public string Description { get; set; } = "Add card info here...";
    public long BoardListId { get; set; } // foreign key id
    public BoardList BoardList { get; set; } = null!; // foreign key reference
}