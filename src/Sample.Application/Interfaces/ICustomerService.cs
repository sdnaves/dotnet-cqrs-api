using FluentValidation.Results;
using Sample.Application.EventSourcedNormalizers;
using Sample.Application.ViewModels;

namespace Sample.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetAll();
        Task<CustomerViewModel> GetById(string id);
        Task<ValidationResult> Create(CustomerViewModel customerViewModel);
        Task<ValidationResult> Update(CustomerViewModel customerViewModel);
        Task<ValidationResult> Delete(string id);
        Task<IList<CustomerHistoryData>> GetAllHistory(string id);
    }
}
