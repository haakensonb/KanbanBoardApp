using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class BackendContext : DbContext
{
    public BackendContext(DbContextOptions<BackendContext> options)
        : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<BoardList> BoardLists { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
}