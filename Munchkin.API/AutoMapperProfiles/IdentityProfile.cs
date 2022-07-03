using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Munchkin.API.DTOs.Identity;

namespace Munchkin.API.AutoMapperProfiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<SignInResult, SignInResultDto>();
            CreateMap<IdentityResult, IdentityResultDto>();
            CreateMap<IdentityError, IdentityErrorDto>();
        }
    }
}
