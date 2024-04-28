using AutoMapper;
using MongoDB.Bson;
using Sample.Application.ViewModels;
using Sample.Domain.Commands.Account;
using Sample.Infra.CrossCutting.Security.Utilities;

namespace Sample.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            // Account
            CreateMap<CreateAccountViewModel, CreateAccountCommand>()
                .ConstructUsing(c => new CreateAccountCommand(c.Name, c.Email, c.Password));

            CreateMap<AccountViewModel, UpdateAccountCommand>()
                .ConstructUsing(c => new UpdateAccountCommand(ObjectId.Parse(c.Id), c.Name, c.Email, c.PasswordHash));
        }
    }
}
