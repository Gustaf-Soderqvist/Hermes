﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
    }
}
