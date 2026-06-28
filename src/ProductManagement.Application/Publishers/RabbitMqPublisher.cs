using System.Text;
using System.Text.Json;
using ProductManagement.Application.Interfaces;
using RabbitMQ.Client;

namespace ProductManagement.Application.Publishers;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly string _hostName = "localhost";
    private readonly int _port = 5672;

    public async Task PublishAsync<T>(T @event)
    {
        try
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName, 
                Port = _port, 
                UserName = "guest", 
                Password = "guest"
            };
            
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: "products.events",
                type: ExchangeType.Direct,
                durable: true
            );

            var json = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "products.events",
                routingKey: typeof(T).Name,
                body: new ReadOnlyMemory<byte>(body)
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}