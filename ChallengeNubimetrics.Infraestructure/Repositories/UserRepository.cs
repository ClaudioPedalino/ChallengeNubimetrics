using ChallengeNubimetrics.Domain.Entities;
using ChallengeNubimetrics.Infraestructure.Interfaces;
using ChallengeNubimetrics.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
            => _context.Users
                .Where(x => x.DeleteAt == null)
                .AsNoTracking();


        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
