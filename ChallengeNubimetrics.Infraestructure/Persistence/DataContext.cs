using ChallengeNubimetrics.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Infraestructure.Persistence
{
    public class DataContext : DbContext
    {
        private readonly UserManager<User> _userManager;

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }

    }
}
