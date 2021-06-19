using ChallengeNubimetrics.Application.Extensions;
using ChallengeNubimetrics.Application.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Services
{
    public class CacheHostedService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private IMemoryCache _cache;
        private Timer _timer;

        public CacheHostedService(IServiceScopeFactory scopeFactory, IMemoryCache cache)
        {
            _scopeFactory = scopeFactory;
            _cache = cache;
        }

        private void Execute(object state)
        {
            _cache.Clear();
            Printer.Print("CACHE WAS RECYCLED", ConsoleColor.Cyan);
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            if (isDevelopment)
                _timer = new Timer(Execute, null, TimeSpan.Zero, TimeSpan.FromSeconds(600));
            else
                _timer = new Timer(Execute, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

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
