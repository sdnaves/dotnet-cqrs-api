using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sample.Domain.Core.Interfaces;
using Sample.Domain.Core.Models;
using Sample.Infra.CrossCutting.Attributes;
using Sample.Infra.CrossCutting.Mediator;
using System.Reflection;

namespace Sample.Infra.Data.Contexts
{
    public class SampleContext : IUnitOfWork, IDisposable
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly List<(Entity Document, Func<Task> Command)> _commands = [];

        private IClientSessionHandle? Session { get; set; }

        public SampleContext(IConfiguration configuration, IMediatorHandler mediatorHandler)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            ArgumentNullException.ThrowIfNull(mediatorHandler);

            _client = new MongoClient(configuration.GetConnectionString("Default"));

            if (_client == null) throw new Exception($"{nameof(_client)} must not be null");

            // set the databaseName
            _database = _client.GetDatabase("sample-db");

            _mediatorHandler = mediatorHandler;
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            var attribute = typeof(TDocument).GetCustomAttribute<BsonCollectionAttribute>(true);

            return attribute switch
            {
                null => throw new Exception($"The {nameof(BsonCollectionAttribute)} is required"),
                _ => _database.GetCollection<TDocument>(attribute.CollectionName),
            };
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                using (Session = await _client.StartSessionAsync())
                {
                    Session.StartTransaction();

                    var commandTasks = _commands.Select(s => s.Command());

                    await Task.WhenAll(commandTasks);

                    await Session.CommitTransactionAsync();
                }

                return _commands.Count;
            }
            finally
            {
                _commands.Clear();
            }
        }

        public async Task<bool> Commit()
        {
            var success = await SaveAsync() > 0;

            await _mediatorHandler.PublishDomainEvents(_commands.Select(s => s.Document)).ConfigureAwait(false);

            return success;
        }

        public void AddCommand(Func<Task> command, Entity document)
        {
            _commands.Add((document, command));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents(this IMediatorHandler mediator, IEnumerable<Entity> entities)
        {
            var domainEntities = entities.Where(w => w.DomainEvents != null && w.DomainEvents.Count != 0);

            var domainEvents = domainEntities.SelectMany(s => s.DomainEvents).ToList();

            domainEntities.ToList().ForEach(entity => entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await mediator.PublishEvent(domainEvent);
            });

            await Task.WhenAll(tasks);
        }
    }
}
