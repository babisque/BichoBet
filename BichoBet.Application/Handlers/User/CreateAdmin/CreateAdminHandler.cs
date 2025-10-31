using AutoMapper;
using BichoBet.Domain.Entities;
using BichoBet.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BichoBet.Application.Handlers.User.CreateAdmin;

public class CreateAdminHandler(IMapper mapper,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<Guid>> roleManager
    ) : IRequestHandler<CreateAdminCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserAlreadyExists",
                Description = "A user with this email already exists."
            });
        
        var user = mapper.Map<ApplicationUser>(request);
        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
            return createResult;
        
        const string adminRoleName = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            await userManager.DeleteAsync(user);
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = $"The role '{adminRoleName}' does not exist."
            });
        }
        
        var addToRoleResult = await userManager.AddToRoleAsync(user, adminRoleName);
        if (addToRoleResult.Succeeded) return createResult;
        
        await userManager.DeleteAsync(user);
        return IdentityResult.Failed(new IdentityError
        {
            Code = "AddToRoleFailed",
            Description = "Failed to add user to role."
        });
    }
}