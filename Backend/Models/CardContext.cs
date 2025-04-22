using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class CardContext : DbContext
{
    public CardContext(DbContextOptions<CardContext> options)
        : base(options)
    {
    }

    public DbSet<Card> Cards { get; set; } = null!;
}