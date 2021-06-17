using Newtonsoft.Json;
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
    }
}
