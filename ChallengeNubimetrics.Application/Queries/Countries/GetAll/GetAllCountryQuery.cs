using ChallengeNubimetrics.Application.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetAll
{
    public record GetAllCountryQuery : IRequest<IEnumerable<GetAllCountryResponse>>, ICacheable
    {
        public string CacheKey => $"{GetType().Name}";
    }
}