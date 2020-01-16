using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Comment> Comments { get; set; }
    }
}
