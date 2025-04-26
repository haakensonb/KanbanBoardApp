namespace Backend.Models;

public class CardDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = "Example Card";
    public string Description { get; set; } = "Add card info here...";
    public long BoardListId { get; set; } // foreign key id

    public static CardDTO ToDTO(Card card)
    {
        return new CardDTO { Id = card.Id, Title = card.Title, Description = card.Description, BoardListId = card.BoardListId };
    }
}