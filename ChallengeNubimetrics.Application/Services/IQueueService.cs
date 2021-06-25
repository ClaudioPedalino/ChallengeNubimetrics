using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Services
{
    public interface IQueueService
    {
        Task Consume(string queueName);
        Task Produce<T>(T message, string queueName);
    }
}