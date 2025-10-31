using BichoBet.Domain.Enum;

namespace BichoBet.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BankAccountId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public TransactionStatus Status { get; set; }
    public string IpAddress { get; set; }
    public string DeviceFingerprint { get; set; }
}