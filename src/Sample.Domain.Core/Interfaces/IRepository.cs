using MongoDB.Bson;
using Sample.Domain.Core.Models;
using System.Linq.Expressions;

namespace Sample.Domain.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<T>> FilterByAsync(Expression<Func<T, bool>> filterExpression);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(ObjectId id);
        Task<T> GetByIndexAsync(string name, object value);

        void Add(T document);
        void Update(T document);
        void Delete(T document);
    }
}
