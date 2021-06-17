using MediatR;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetAll
{
    public class GetAllCountryQuery : IRequest<IEnumerable<GetAllCountryResponse>>
    {
    }
}
