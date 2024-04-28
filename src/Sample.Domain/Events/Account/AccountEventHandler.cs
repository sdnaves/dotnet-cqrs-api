using MediatR;

namespace Sample.Domain.Events.Account
{
    public class AccountEventHandler : EventHandler,
                                       INotificationHandler<AccountCreatedEvent>,
                                       INotificationHandler<AccountUpdatedEvent>,
                                       INotificationHandler<AccountDeletedEvent>
    {
        public Task Handle(AccountCreatedEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(AccountUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(AccountDeletedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}
