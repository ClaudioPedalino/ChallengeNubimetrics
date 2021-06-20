using System;

namespace ChallengeNubimetrics.Application.Models.Common
{
    public class LoggedRequestResponse
    {
        public LoggedRequestResponse(LoggedRequest request, LoggedResponse response)
        {
            TraceId = Guid.NewGuid();
            Request = request;
            Response = response;
            Date = DateTime.UtcNow.AddHours(-3);
        }

        public Guid TraceId { get; init; }
        public LoggedRequest Request { get; init; }
        public LoggedResponse Response { get; init; }
        public DateTime Date { get; init; }
    }

    public class LoggedRequest
    {
        public string IpRequested { get; init; }
        public string RequestTo { get; init; }
        public string HttpMethod { get; init; }
        public string Path { get; init; }
        public string QueryParams { get; init; }
    }

    public class LoggedResponse
    {
        public int StatusCode { get; init; }
        public string Body { get; init; }
    }
}
