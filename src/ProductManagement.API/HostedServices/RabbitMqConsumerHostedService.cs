using ProductManagement.API.Consumers;

namespace ProductManagement.API.HostedServices;

public class RabbitMqConsumerHostedService(ProductEventConsumer consumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await consumer.StartAsync();
        
        // Mantém rodando até parar
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}