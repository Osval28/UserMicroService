using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UserService.Infrastructure;
using UserService.Infrastructure.Repositories;
public class InvitationConsumer
{
    private readonly InvitationRepository _invitationRepository;

    public InvitationConsumer(InvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository;
    }

    public void Start()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "invitation-sync",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var invitation = JsonSerializer.Deserialize<InvitationCode>(message);

            _invitationRepository.Create(invitation);
        };

        channel.BasicConsume(queue: "invitation-sync",
                             autoAck: true,
                             consumer: consumer);
    }
}
