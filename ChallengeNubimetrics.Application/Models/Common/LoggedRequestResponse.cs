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

        public Guid TraceId { get; set; }
        public LoggedRequest Request { get; set; }
        public LoggedResponse Response { get; set; }
        public DateTime Date { get; set; }
    }

    public class LoggedRequest
    {
        public string IpRequested { get; set; }
        public string RequestTo { get; set; }
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public string QueryParams { get; set; }
    }

    public class LoggedResponse
    {
        public int StatusCode { get; set; }
        public string Body { get; set; }
    }
}
