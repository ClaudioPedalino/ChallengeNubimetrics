using ChallengeNubimetrics.Application.Helpers;
using ChallengeNubimetrics.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.PipelineBehaviors
{
    public class CacheBehaviour<TRquest, TResponse> : IPipelineBehavior<TRquest, TResponse>
        where TRquest : ICacheable
    {
        private readonly IMemoryCache _cache;

        public CacheBehaviour(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRquest request,
                                            CancellationToken cancellationToken,
                                            RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            

            /// If cached
            TResponse response;
            if (_cache.TryGetValue(request.CacheKey, out response))
            {
                Printer.Print($"Returned cached value for { requestName}", backgroundColor: ConsoleColor.Blue);
                return response;
            }

            /// If not, caching
            MemoryCacheEntryOptions cacheExpirationOptions = new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                Priority = CacheItemPriority.Normal
            };

            Printer.Print($"Caching {requestName}", backgroundColor: ConsoleColor.Red);
            Printer.Print($"{requestName} CACHE KEY: {request.CacheKey} is not cached, executing request", backgroundColor: ConsoleColor.Cyan);
            response = await next();
            _cache.Set(request.CacheKey, response, cacheExpirationOptions);
            return response;
        }
    }
}
