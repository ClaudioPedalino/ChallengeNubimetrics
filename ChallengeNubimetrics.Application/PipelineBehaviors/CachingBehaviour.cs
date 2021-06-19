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
    public class CachingBehaviour<TRquest, TResponse> : IPipelineBehavior<TRquest, TResponse>
        where TRquest : ICacheable
    {
        private readonly IMemoryCache _cache;

        public CachingBehaviour(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRquest request,
                                            CancellationToken cancellationToken,
                                            RequestHandlerDelegate<TResponse> next)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            var requestName = request.GetType();
            Console.WriteLine($"Caching {requestName} <><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>");


            /// If cached
            TResponse response;
            if (_cache.TryGetValue(request.CacheKey, out response))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Returned cached value for {requestName}");
                return response;
            }

            /// If not, caching
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{requestName} CACHE KEY: {request.CacheKey} is not cached, executing request");
            response = await next();
            _cache.Set(request.CacheKey, response);
            return response;
        }
    }
}
