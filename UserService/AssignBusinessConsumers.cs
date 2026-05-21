using RabbitMQ.Client;
using UserService.Infrastructure.Repositories;

public class AssignBusinessConsumer
{
    private readonly UserRepository _userRepository;

    public AssignBusinessConsumer(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Start()
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
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var assignData = JsonSerializer.Deserialize<AssignBusinessDTO>(message);

            _userRepository.AssignBusiness(assignData.UserId, assignData.BusinessId, assignData.RoleId);
        };

        channel.BasicConsume(queue: "assign-business",
                             autoAck: true,
                             consumer: consumer);
    }
}
