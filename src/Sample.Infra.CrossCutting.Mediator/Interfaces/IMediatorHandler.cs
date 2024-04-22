using FluentValidation.Results;
using Sample.Infra.CrossCutting.Mediator.Models;

namespace Sample.Infra.CrossCutting.Mediator.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
