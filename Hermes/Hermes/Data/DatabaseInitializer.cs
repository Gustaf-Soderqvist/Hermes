using Hermes.Core;
using Hermes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        public readonly ApplicationDbContext _context;
        public readonly IAccountManager _accountManager;
        public readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Adding users");

                await CreateUserAsync("Gustaf", "hermesT@123", "Inbuilt Administrator", "gustafs@hermes.se", "0706000000");
                await CreateUserAsync("Test", "hermesT@123", "Inbuilt Standard User", "testuser@hermes.se", "0706000000");

                _logger.LogInformation("Users has been added");
            }
            throw new NotImplementedException();
        }

        private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, password);

            if (!result.Succeeded)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");


            return applicationUser;
        }
    }
}
