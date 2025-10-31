using AutoMapper;
using BichoBet.Application.Handlers.User.CreateAdmin;
using BichoBet.Domain.Entities;

namespace BichoBet.Application.Profiles;

public class ApplicationUserMapper : Profile
{
    public ApplicationUserMapper()
    {
        CreateMap<CreateAdminCommand, ApplicationUser>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.DateOfBirth));
    }
}