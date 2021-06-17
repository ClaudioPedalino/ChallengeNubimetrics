using ChallengeNubimetrics.Domain.Entities;
using ChallengeNubimetrics.Infraestructure.Interfaces;
using ChallengeNubimetrics.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.Where(x => x.DeleteAt == null).ToListAsync();
            return result;
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
