using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UserService.Application.DTO_s;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;

namespace UserService
{
    public class AssignBusinessConsumerHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AssignBusinessConsumerHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "assign-business",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                using var scope = _scopeFactory.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var assignData = JsonSerializer.Deserialize<AssignBusinessDTO>(message);

                userRepository.AssignBusiness(assignData.UserId, assignData.BusinessId, assignData.RoleId);
            };

            channel.BasicConsume(queue: "assign-business",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}