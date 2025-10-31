using BichoBet.Domain.Enum;

namespace BichoBet.Domain.Entities;

public class BankAccount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public BankAccountType Type { get; set; }
    public string Key { get; set; }
    public string TitularAccount { get; set; }
    public bool Enable { get; set; }
}