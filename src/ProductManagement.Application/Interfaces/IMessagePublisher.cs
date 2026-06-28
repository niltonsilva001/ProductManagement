namespace ProductManagement.Application.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T @event);
}
