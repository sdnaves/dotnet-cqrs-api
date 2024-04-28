using AutoMapper;
using Sample.Application.ViewModels;
using Sample.Domain.Models;

namespace Sample.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            // Account
            CreateMap<Account, AccountViewModel>()
                .ForMember(dst => dst.PasswordHash, act => act.MapFrom(src => src.Password));
        }
    }
}
