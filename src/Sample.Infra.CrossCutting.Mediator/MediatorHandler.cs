using FluentValidation.Results;
using MediatR;
using Sample.Infra.CrossCutting.Mediator.Interfaces;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Infra.CrossCutting.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        private readonly IEventStore _eventStore;

        public MediatorHandler(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;

            _eventStore = eventStore;
        }

        public virtual Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public virtual Task PublishEvent<T>(T @event) where T : Event
        {
            _eventStore?.Save(@event);

            return _mediator.Publish(@event);
        }
    }
}
