using ChallengeNubimetrics.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Infraestructure.Persistence
{
    public static class SeedDatabaseInitialData
    {
        public static void SeedUsers(UserManager<User> _userManager)
        {
            var testPassword = "Alagrandelepusecuca#123";

            var seedUsers = new List<User>()
            {
                new User{ FirstName = "Rocket", LastName = "Raccoon", Email = "rocketraccoon@gmail.com", UserName = "rocketraccoon@gmail.com", CreatedAt = DateTime.UtcNow.AddHours(-3)},
                new User{ FirstName = "Claudio", LastName = "Pedalino", Email = "cpedalino@gmail.com", UserName = "cpedalino@gmail.com", CreatedAt = DateTime.UtcNow.AddHours(-3)}
            };

            foreach (var user in seedUsers)
            {
                IdentityResult result = _userManager.CreateAsync(user, testPassword).Result;
            }
        }
    }

}
