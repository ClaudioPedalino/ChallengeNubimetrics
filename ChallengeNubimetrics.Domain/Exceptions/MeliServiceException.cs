namespace ChallengeNubimetrics.Domain.Exceptions
{
    public class MeliServiceException : CustomApplicationException
    {
        public MeliServiceException(string url, string message = "")
            : base($"Error connecting service {url} {message}") { }
    }
}
