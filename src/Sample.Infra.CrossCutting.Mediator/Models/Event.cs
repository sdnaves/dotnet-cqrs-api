using MediatR;

namespace Sample.Infra.CrossCutting.Mediator.Models
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
