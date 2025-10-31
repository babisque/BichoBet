using Microsoft.AspNetCore.Identity;

namespace BichoBet.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public string CPF { get; set; }
    public DateTime BirthDate { get; set; }
    public string StatusKYC { get; set; }
    public decimal Balance { get; set; }
}