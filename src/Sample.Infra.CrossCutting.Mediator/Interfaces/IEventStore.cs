using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Infra.CrossCutting.Mediator.Interfaces
{
    public interface IEventStore
    {
        void Save<T>(T @event) where T : Event;
    }
}
