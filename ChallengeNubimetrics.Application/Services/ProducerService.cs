using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IConfiguration _configuration;

        public ProducerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Task Produce<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration.GetValue<string>("RabbitMQ:Hostname"))
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare(queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", queueName, null, body);
            
            return Task.CompletedTask;
        }


        public async Task Consume(string queueName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration.GetValue<string>("RabbitMQ:Hostname"))
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queueName, true, consumer);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(message);
                Console.ResetColor();
            };

        }
    }
}
