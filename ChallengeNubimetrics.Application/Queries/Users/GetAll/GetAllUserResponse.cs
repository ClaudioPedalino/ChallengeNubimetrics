using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ChallengeNubimetrics.Application.Queries.Users.GetAll
{
    [DebuggerDisplay("{Email} - {Nombre} {Apellido}")]
    public class GetAllUserResponse
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        
        private string GetDebuggerDisplay() => ToString();
    }
}
