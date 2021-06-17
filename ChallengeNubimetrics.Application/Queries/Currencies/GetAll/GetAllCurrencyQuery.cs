using MediatR;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Queries.Currencies.GetAll
{
    public class GetAllCurrencyQuery : IRequest<IEnumerable<GetAllCurrencyResponse>>
    {
    }
}
