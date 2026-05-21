using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;

public class InvitationConsumerHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InvitationConsumerHostedService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "invitation-sync", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<InvitationRepository>();

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var invitation = JsonSerializer.Deserialize<InvitationCode>(message);
            repo.Create(invitation);
        };

        channel.BasicConsume(queue: "invitation-sync", autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}