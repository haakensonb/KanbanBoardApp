using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class BoardListContext : DbContext
{
    public BoardListContext(DbContextOptions<BoardListContext> options)
        : base(options)
    {
    }

    public DbSet<BoardList> BoardLists { get; set; } = null!;
}