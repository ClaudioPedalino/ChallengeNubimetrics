using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;

        public QueueService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task Produce<T>(T message, string queueName)
        {
            SetConnection(out IConnection connection, out IModel channel);

            channel.QueueDeclare(queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", queueName, null, body);

            return Task.CompletedTask;
        }

        private void SetConnection(out IConnection connection, out IModel channel)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration.GetValue<string>("RabbitMQ:Hostname"))
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public async Task Consume(string queueName)
        {
            SetConnection(out IConnection connection, out IModel channel);

            channel.QueueDeclare(queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(message);
                Console.ResetColor();
            };

            channel.BasicConsume(queueName, true, consumer);
        }
    }
}