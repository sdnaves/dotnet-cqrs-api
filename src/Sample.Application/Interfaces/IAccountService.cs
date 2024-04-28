using FluentValidation.Results;
using Sample.Application.ViewModels;
using Sample.Domain.Core.Models;

namespace Sample.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> GetAll();
        Task<AccountViewModel> GetById(string id);
        Task<AccountViewModel> GetByEmail(string email);
        Task<ValidationResult> Create(CreateAccountViewModel accountViewModel);
        Task<ValidationResult> Update(AccountViewModel accountViewModel);
        Task<ValidationResult> Delete(string id);
        Task<IList<HistoryData<AccountViewModel>>> GetAllHistory(string id);
    }
}
