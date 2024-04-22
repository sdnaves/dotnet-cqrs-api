using AutoMapper;
using MongoDB.Bson;
using Sample.Application.ViewModels;
using Sample.Domain.Commands.Customer;

namespace Sample.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, CreateCustomerCommand>()
                .ConstructUsing(c => new CreateCustomerCommand(c.Name, c.Email, c.BirthDate));

            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(ObjectId.Parse(c.Id), c.Name, c.Email, c.BirthDate));
        }
    }
}
