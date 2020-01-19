using Hermes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hermes.Core
{
    public interface IAccountManager
    {
        Task<(bool Succeeded, string[] Errors)> CreateUserAsync(ApplicationUser user, string password);
    }
}