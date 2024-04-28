using Sample.Domain.Core.Models;

namespace Sample.Application.Interfaces
{
    public interface IHistoryService
    {
        Task<IList<HistoryData<T>>> GetHistoryData<T>(string aggregatedId) where T : new();
    }
}
