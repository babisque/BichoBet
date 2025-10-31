using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BichoBet.Application.Handlers.User.CreateAdmin;

public abstract class CreateAdminCommand : IRequest<IdentityResult>
{
    public string FirstNam { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
}