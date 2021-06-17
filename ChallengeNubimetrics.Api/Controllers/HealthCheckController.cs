using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HealthCheckController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("live")]
        public async Task<ActionResult> GetLiveAsync()
            => await CallHealthCheck("live", _configuration);


        [HttpGet("ready")]
        public async Task<ActionResult> GetReadyAsync()
            => await CallHealthCheck("ready", _configuration);


        [HttpGet("ui-data")]
        public async Task<ActionResult> GetUIDataAsync()
            => await CallHealthCheck("ui", _configuration);


        private async Task<ActionResult> CallHealthCheck(string param, IConfiguration _configuration)
        {
            using HttpClient client = new();

            var uri = _configuration.GetValue<string>("HealthCheckUri") + param;
            HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
