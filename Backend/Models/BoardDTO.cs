using System.Collections;

namespace Backend.Models;

public class BoardDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = "Example Board";
    public IEnumerable<BoardListDTO> BoardLists { get; set; } = new List<BoardListDTO>();

    public static BoardDTO ToDTO(Board board)
    {
        return new BoardDTO
        {
            Id = board.Id,
            Name = board.Name,
            BoardLists = board.BoardLists.Select(b => BoardListDTO.ToDTO(b))
        };
    }
}