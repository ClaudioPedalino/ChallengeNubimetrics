using MediatR;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetById
{
    public class GetCountryByIdQuery : IRequest<GetCountryByIdResponse>
    {
        public GetCountryByIdQuery(string countryId)
        {
            CountryId = countryId;
        }

        /// <summary>
        /// Iso2 Id Format
        /// </summary>
        public string CountryId { get; set; }
    }
}
