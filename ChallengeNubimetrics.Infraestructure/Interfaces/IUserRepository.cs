using ChallengeNubimetrics.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Infraestructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User entity);
    }
}
