using AutoMapper;
using BichoBet.Application.Handlers.User.CreateAdmin;
using BichoBet.Domain.Entities;

namespace BichoBet.Application.Profiles;

public class ApplicationUserMapper : Profile
{
    public ApplicationUserMapper()
    {
        CreateMap<ApplicationUser, CreateAdminCommand>()
            .ReverseMap();
    }
}