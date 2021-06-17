using ChallengeNubimetrics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Infraestructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User entity);
    }
}
