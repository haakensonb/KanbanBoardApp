namespace Backend.Models;

public class BoardList
{
    public long Id { get; set; }
    public string Title { get; set; } = "Example List";
    public ICollection<Card> Cards { get; } = new List<Card>();
    public long BoardId { get; set; } // foreign key id
    public Board Board { get; set; } = null!; // foreign key reference
}