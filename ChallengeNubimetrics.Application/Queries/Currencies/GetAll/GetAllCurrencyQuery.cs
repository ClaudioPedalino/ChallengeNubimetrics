using ChallengeNubimetrics.Application.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Queries.Currencies.GetAll
{
    public record GetAllCurrencyQuery : IRequest<IEnumerable<GetAllCurrencyResponse>>, ICacheable
    {
        public string CacheKey => $"{GetType().Name}";
    }
}