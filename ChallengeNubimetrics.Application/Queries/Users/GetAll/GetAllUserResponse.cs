using System.Diagnostics;

namespace ChallengeNubimetrics.Application.Queries.Users.GetAll
{
    [DebuggerDisplay("{Email} - {Nombre} {Apellido}")]
    public record GetAllUserResponse
    {
        public string Id { get; init; }
        public string Nombre { get; init; }
        public string Apellido { get; init; }
        public string Email { get; init; }

        private string GetDebuggerDisplay() => ToString();
    }
}