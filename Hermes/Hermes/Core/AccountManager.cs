using Hermes.Data;
using Hermes.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Core
{
    public class AccountManager : IAccountManager
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountManager(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;

        }

        public async Task<(bool Succeeded, string[] Errors)> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());


            user = await _userManager.FindByNameAsync(user.UserName);

            if (!result.Succeeded)
            {
                await DeleteUserAsync(user);
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }

            return (true, new string[] { });
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
                return await DeleteUserAsync(user);

            return (true, new string[] { });
        }


        public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }
    }
}
