using ChallengeNubimetrics.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Infraestructure.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns></returns>
        IQueryable<User> GetAll();

        /// <summary>
        /// Update an existing user from database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(User entity);
    }
}