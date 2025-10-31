using BichoBet.Domain.Enum;

namespace BichoBet.Domain.Entities;

public class Award
{
    public Guid Id { get; set; }
    public Guid DrawId { get; set; }
    public int Position { get; set; }
    public Thousands Thousands { get; set; }
}