using ChallengeNubimetrics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNubimetrics.Infraestructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }

    }
}
