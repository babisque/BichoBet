using BichoBet.Domain.Enum;

namespace BichoBet.Domain.Entities;

public class Bet
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DrawId { get; set; }
    public Modality Modality { get; set; }
    public string Content { get; set; }
    public decimal Amount { get; set; }
    public BetStatus Status { get; set; }
    public decimal PrizeValue { get; set; }
    public string IpAddress { get; set; }
    public string DeviceFingerprint { get; set; }
}