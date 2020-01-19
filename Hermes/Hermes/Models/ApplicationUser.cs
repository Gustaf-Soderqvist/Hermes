using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Models
{

    public class ApplicationUser : IdentityUser
    {

        //public virtual string FriendlyName
        //{
        //    get
        //    {
        //        string friendlyName = string.IsNullOrWhiteSpace(FullName) ? UserName : FullName;
        //        return friendlyName;
        //    }
        //}
        //public string FullName { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Like> Likes { get; set; }
    }
}
