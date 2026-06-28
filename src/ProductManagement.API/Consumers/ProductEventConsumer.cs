using System.Text;
using System.Text.Json;
using ProductManagement.Domain.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProductManagement.API.Consumers;

public class ProductEventConsumer
{
    private readonly string _hostName = "localhost";
    private readonly int _port = 5672;

    public async Task StartAsync()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            Port = _port,
            UserName = "guest",
            Password = "guest"
        };

        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(
            queue: "product.created.queue",
            durable: true,
            exclusive: false,
            autoDelete: false
        );
        
        await channel.ExchangeDeclareAsync(
            exchange: "products.events",
            type: ExchangeType.Direct,
            durable: true
        );
        
        await channel.QueueBindAsync(
            queue: "product.created.queue",
            exchange: "products.events",
            routingKey: "ProductCreatedEvent"
        );
        
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
    
            var productEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(json);
    
            // Fazer algo com a mensagem!
            Console.WriteLine($"Produto criado: {productEvent?.ProductName}");
    
            // Confirmar recebimento (acknowledge)
            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync(
            queue: "product.created.queue",
            autoAck: false,
            consumer: consumer
        );
    }
}