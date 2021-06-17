using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Services
{
    public class PrinterHostedService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public PrinterHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        private void Execute(object state)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("Remember drink some water - hosted service :D");
            Console.ResetColor();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Execute, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}