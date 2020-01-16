using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Note { get; set; }
        //public List<Like> Likes { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

