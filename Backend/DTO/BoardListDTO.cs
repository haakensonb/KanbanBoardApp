using Backend.Models;

namespace Backend.DTO;

public record BoardListDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = "Example List";
    public long BoardId { get; set; } // foreign key id
    public IEnumerable<CardDTO> Cards { get; set; } = new List<CardDTO>();

    public static BoardListDTO ToDTO(BoardList boardList)
    {
        return new BoardListDTO
        {
            Id = boardList.Id,
            Title = boardList.Title,
            BoardId = boardList.BoardId,
            Cards = boardList.Cards.Select(c => CardDTO.ToDTO(c))
        };
    }
}