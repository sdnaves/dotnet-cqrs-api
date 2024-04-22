using MediatR;

namespace Sample.Domain.Events.Customer
{
    public class CustomerEventHandler : EventHandler,
                                        INotificationHandler<CustomerCreatedEvent>,
                                        INotificationHandler<CustomerUpdatedEvent>,
                                        INotificationHandler<CustomerDeletedEvent>
    {
        public Task Handle(CustomerCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CustomerUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CustomerDeletedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
