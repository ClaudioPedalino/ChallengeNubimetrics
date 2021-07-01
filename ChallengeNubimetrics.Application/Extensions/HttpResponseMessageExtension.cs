using ChallengeNubimetrics.Domain.Exceptions;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> ReadResponse<T>(this HttpResponseMessage serviceResult)
        {
            var jsonResult = await serviceResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResult);
        }

        public static void ExecuteMeliLogging(this HttpResponseMessage httpMessage, ILogger logger, string url, bool throwEx = false)
        {
            if (httpMessage.StatusCode == HttpStatusCode.NotFound)
                logger.Information($"[{httpMessage.StatusCode}] {new MeliServiceException(url).Message}");
            else
                logger.Error($"[{httpMessage.StatusCode}] {new MeliServiceException(url).Message}");

            if (throwEx)
                throw new CustomApplicationException($":(  Problems into => {url}");
        }
    }
}