using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Hermes.Models
{
    [DataContract(IsReference = true)]
    public class Like
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [DataMember]
        public Comment Comment { get; set; }

        [DataMember]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [DataMember]
        public ApplicationUser User { get; set; }
    }
}
