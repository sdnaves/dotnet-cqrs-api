using MongoDB.Bson;
using MongoDB.Driver;
using Sample.Domain.Core.Interfaces;
using Sample.Domain.Core.Models;
using Sample.Infra.Data.Contexts;
using System.Linq.Expressions;

namespace Sample.Infra.Data.Repositories
{
    public abstract class BaseRepository<TDocument> : IRepository<TDocument> where TDocument : Entity
    {
        protected readonly SampleContext Db;
        protected readonly IMongoCollection<TDocument> DbSet;

        public BaseRepository(SampleContext db)
        {
            ArgumentNullException.ThrowIfNull(db);

            Db = db;
            DbSet = db.GetCollection<TDocument>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public virtual async Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return (await DbSet.FindAsync(filterExpression)).ToEnumerable();
        }

        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await DbSet.Find(filterExpression).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            var items = await DbSet.Find(item => true).ToListAsync();

            return items;
        }

        public virtual async Task<TDocument> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", id);

            return await DbSet.Find(filter).SingleOrDefaultAsync();
        }

        public virtual async Task<TDocument> GetByIndexAsync(string name, object value)
        {
            var filter = Builders<TDocument>.Filter.Eq(name, value);

            return await DbSet.Find(filter).SingleOrDefaultAsync();
        }

        public virtual void Add(TDocument document)
        {
            Db.AddCommand(() => DbSet.InsertOneAsync(document), document);
        }

        public virtual void Update(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", document.Id);

            Db.AddCommand(() => DbSet.ReplaceOneAsync(filter, document), document);
        }

        public virtual void Delete(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", document.Id);

            Db.AddCommand(() => DbSet.DeleteOneAsync(filter), document);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
