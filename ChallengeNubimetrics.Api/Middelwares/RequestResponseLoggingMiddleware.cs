using ChallengeNubimetrics.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.IO;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Api.Middelwares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = SetRequest(context);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using var responseBody = new MemoryStream();

            //...and use that for the temporary response body
            context.Response.Body = responseBody;

            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            var response = await SetResponse(context.Response);

            var loggedRequestResponse = new LoggedRequestResponse(request, response);

            _logger.Information(JsonConvert.SerializeObject(loggedRequestResponse, Formatting.Indented));

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private LoggedRequest SetRequest(HttpContext context)
            => new()
            {
                IpRequested = context.Connection.RemoteIpAddress.ToString(),
                HttpMethod = context.Request.Method,
                RequestTo = $"{context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Request.QueryString}",
                Path = context.Request.Path,
                QueryParams = context.Request.QueryString.Value,
            };

        private async Task<LoggedResponse> SetResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string bodyString = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            return new LoggedResponse()
            {
                StatusCode = response.StatusCode,
                Body = bodyString
            };
        }
    }
}
